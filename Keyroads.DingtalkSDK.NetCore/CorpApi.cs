using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Keyroads.DingtalkSDK
{
    public static class CorpApi
    {
        public static async Task<DepartmentListResponse> GetDepartmentList(BaseGetContactRequest request)
        {
            var result = await CommonApi.AccessDingtalkServerAsync<DepartmentListResponse>(
               $"https://oapi.dingtalk.com/department/list?access_token={request.access_token}&lang={request.lang}&id={request.id}",
               null, "GET");
            return result;
        }

        public static async Task<UserListResponse> GetUserList(UserListGetRequest request)
        {
            var result = await CommonApi.AccessDingtalkServerAsync<UserListResponse>(
                $"https://oapi.dingtalk.com/user/list?access_token={request.access_token}&department_id={request.department_id}" ,//+
                //$"&lang={request.lang}&offset={request.offset}&size={request.size}&order={request.order}",
                null, "GET");
            return result;
        }

        /// <summary>
        /// 得到某个部门下面所有（包括子部门）的成员列表
        /// </summary>
        /// <param name="request">request.id为部门id</param>
        /// <returns></returns>
        public static async Task<UserListResponse> GetAllUserList(BaseGetContactRequest request)
        {
            var result = await GetUserList(
                    new UserListGetRequest { access_token = request.access_token, department_id = long.Parse(request.id) });
            if (result.errcode != 0) return result;

            var resultGetDepartmentList = await GetDepartmentList(request);
            foreach (var info in resultGetDepartmentList.department)
            {
                var resultGetUserList = await GetUserList(
                    new UserListGetRequest { access_token = request.access_token, department_id = info.id });
                if (resultGetUserList.errcode != 0)
                {
                    return resultGetUserList;
                }
                result.userList.AddRange(resultGetUserList.userList);
            }
            return result;
        }

        public static async Task<GetJsApiTicketResponse> GetJsApiTicketAsync(string accessToken)
        {
            return await CommonApi.AccessDingtalkServerAsync<GetJsApiTicketResponse>(
                $"https://oapi.dingtalk.com/get_jsapi_ticket?access_token={accessToken}&type=jsapi", null, "GET");
        }

        public static async Task<GetUserInfoResponse> GetUserInfo(string accessToken, string code)
        {
            return await CommonApi.AccessDingtalkServerAsync<GetUserInfoResponse>(
                $"https://oapi.dingtalk.com/user/getuserinfo?access_token={accessToken}&code={code}", null, "GET");
        }

        public static async Task<string> GetToken(string corpId, string corpSecret, IAccessTokenCacher cacher = null)
        {
            if (cacher == null) cacher = new MemoryAccessTokenCacher();
            var token = await cacher.GetAsync(corpId, corpSecret);
            if (!string.IsNullOrEmpty(token)) return token;
            var result = await CommonApi.AccessDingtalkServerAsync<GetTokenResponse>(
                $"https://oapi.dingtalk.com/gettoken?corpid={corpId}&corpsecret={corpSecret}", null, "GET");
            if (result.errcode == 0)
            {
                await cacher.SetAsync(corpId, corpSecret, result.access_token, DateTime.Now.AddSeconds(7200));
                return result.access_token;
            }
            throw new Exception($"errcode: {result.errcode}, errmsg: {result.errmsg}");
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021302.Web
{
    public class ApiResult
    {
        /// <summary>
        /// 0: Ko thành công, 1: thành công
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Thông báo lỗi
        /// </summary>
        public string Msg { get; set; }

        public static ApiResult CreateFailResult(string msg)
        {
            return new ApiResult() { Code = 0, Msg = msg };
        }

        public static ApiResult CreateSuccessResult(string msg = "")
        {
            return new ApiResult() { Code = 1, Msg = msg };
        }
    }
}
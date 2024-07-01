using Banking.Core.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Core.Model
{
    public class UserServiceResponse: BaseServiceResponse
    {
        private UserViewDTO? _userViewDTO { get; set; }

        public UserServiceResponse()
        {
        }

        public UserServiceResponse(int httpStatusCode, int returnCode, string message, UserViewDTO userViewDTO) : base(
            httpStatusCode, returnCode, message)
        {
            _userViewDTO = userViewDTO;
        }

        public UserServiceResponse(int httpStatusCode, int returnCode, string message) : base(httpStatusCode,
            returnCode, message)
        {
        }

        public UserServiceResponse(int httpStatusCode, string message) : base(httpStatusCode, message)
        {
            ReturnCode = httpStatusCode;
        }

        public UserServiceResponse(UserViewDTO userViewDTO)
        {
            _userViewDTO = userViewDTO;
        }
    }
}

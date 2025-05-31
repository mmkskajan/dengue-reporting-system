using System;
using System.Collections.Generic;
using System.Text;

namespace CIDRS.Shared.Common.Api.Models
{
    public class ResponseResult<TResult>
    {
        public IEnumerable<string> Errors { get; set; }

        public TResult Result { get; set; }

        public bool Succeeded { get; set; }

    }
}

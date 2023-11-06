using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mYSelfERPWeb.Services
{
    public interface ISMSService
    {
        Task<string> SendSmsAsync(string contacts, string message);
    }
}

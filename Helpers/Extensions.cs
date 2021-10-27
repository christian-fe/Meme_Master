using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memes.Helpers
{
    public static class Extensions
    {
        public static void AddAppicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Application-Expose-Headers", "Application-Error");
            response.Headers.Add("Application-Control-Allow-Origin", "*");
        }
    }
}

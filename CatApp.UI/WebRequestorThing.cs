using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Windows8_MyFirstApp
{
    public class WebRequestorThing
    {
        public async Task<bool> GetEventsAsync()
        {
            var targetUri = "https://www.eventbriteapi.com/v3/events/10584525601/?token=BKKRDKVUVRC5WG4HAVLT";
            var request = HttpWebRequest.Create(targetUri);

            try
            {
                var response = await request.GetResponseAsync();

                using (var responseStream = response.GetResponseStream())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

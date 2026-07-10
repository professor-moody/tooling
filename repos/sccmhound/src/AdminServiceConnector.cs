using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Security.Policy;
using SCCMHound.src.models;

namespace SCCMHound
{

    public class AdminServiceConnector
    {
        String baseUrl;
        SCCMConnectorOptions sccmConnectorOptions;

        private AdminServiceConnector(string baseUrl, SCCMConnectorOptions sccmConnectorOptions)
        {
            this.baseUrl = baseUrl;
            this.sccmConnectorOptions = sccmConnectorOptions;
        }

        private HttpClientHandler newRequestHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
            

            if (this.sccmConnectorOptions is not null)
            {
                handler.Credentials = new NetworkCredential(this.sccmConnectorOptions.username, this.sccmConnectorOptions.password, this.sccmConnectorOptions.domain);
            }
            else
            {
                handler.Credentials = CredentialCache.DefaultCredentials;
                handler.UseDefaultCredentials = true;
            }

            return handler;
        }

        public HttpResponseMessage GetAsync(string url)
        {
            using (var client = new HttpClient(newRequestHandler()))
            {
                return client.GetAsync(this.baseUrl + url).Result;
            }
        }

        public HttpResponseMessage PostAsync(string url, HttpContent content)
        {
            using (var client = new HttpClient(newRequestHandler()))
            {
                return client.PostAsync(this.baseUrl + url, content).Result;
            }
        }

        public static AdminServiceConnector CreateInstance(string mgmtServer, SCCMConnectorOptions sccmConnectorOptions)
        {
            string url = string.Format("https://{0}/AdminService/wmi/SMS_Collection", mgmtServer);


            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
            
            if (sccmConnectorOptions is not null)
            {
                handler.Credentials = new NetworkCredential(sccmConnectorOptions.username, sccmConnectorOptions.password, sccmConnectorOptions.domain);
            }
            else
            {
                handler.Credentials = CredentialCache.DefaultCredentials;
                handler.UseDefaultCredentials = true;
            }
            

            try
            {
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Negotiate");

                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return new AdminServiceConnector(string.Format("https://{0}/AdminService/v1.0/", mgmtServer), sccmConnectorOptions);
                    }
                    else
                    {
                        Console.WriteLine("Failed to retrieve data");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send data");
                return null;
            }

        }
    }
}

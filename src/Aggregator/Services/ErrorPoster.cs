using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Aggregator.Models;
using Aggregator.Services.Interfaces;

namespace Aggregator.Services
{
    public class ErrorPoster : IErrorPoster
    {
        private readonly IWebClientWrapper _webClientWrapper;

        public ErrorPoster(IWebClientWrapper webClientWrapper)
        {
            _webClientWrapper = webClientWrapper;
        }

        public void Post(IEnumerable<string> urls, ErrorInformation errorInfo)
        {
            foreach (var url in urls)
            {
                var valuesToSend = new NameValueCollection()
                {
                    {"errorId", errorInfo.ErrorId},
                    {"error", errorInfo.Error},
                    {"infoUrl", errorInfo.InfoUrl},
                    {"sourceId", errorInfo.SourceId}
                };

                try
                {
                    _webClientWrapper.UploadValues(url, valuesToSend);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
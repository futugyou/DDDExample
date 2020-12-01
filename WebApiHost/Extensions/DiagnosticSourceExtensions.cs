
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiHost.Extensions
{
    public sealed class DiagnosticCollector
    {
        [DiagnosticName("ReveiveRequest")]
        public void OnReveiveRequest(HttpRequestMessage httpRequest, long timestamp)
        {
            Console.WriteLine($"Reveice request url:{httpRequest.RequestUri} ; Timestamp:{timestamp}");
        }

        [DiagnosticName("SendReply")]
        public void OnSendReply(HttpResponseMessage httpResponse, TimeSpan elaped)
        {
            Console.WriteLine($"send reply status code:{httpResponse.StatusCode} ; Elaped:{elaped}");
        }
    }

    public sealed class DiagnosticObserver
    {
        public static readonly DiagnosticObserver Instance = new DiagnosticObserver();
        static readonly DiagnosticListener source = new DiagnosticListener("Web");
        private DiagnosticObserver() { }
        public void RegisteDiagnosticObserver()
        {
            //var source = new DiagnosticListener("Web");
            var stopwatch = Stopwatch.StartNew();
            if (source.IsEnabled("ReveiveRequest"))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://www.baidu.com");
                source.Write("ReveiveRequest", new { HttpRequest = request, Timestamp = Stopwatch.GetTimestamp() });
            }
            if (source.IsEnabled("SendReply"))
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                source.Write("SendReply", new { HttpResponse = response, Elaped = stopwatch.Elapsed });
            }
        }

    }
    public static class DiagnosticSourceExtensions
    {
        public static IApplicationBuilder UseDiagnosticListener(this IApplicationBuilder builder, IConfiguration configuration = null)
        {
            return builder.UseMiddleware<ApmMiddleware>();
        }

        internal class ApmMiddleware
        {
            private readonly RequestDelegate _next;
            public ApmMiddleware(
                RequestDelegate next)
            {
                RegisteDiagnosticObservable();
                _next = next;
            }


            private void RegisteDiagnosticObservable()
            {
                DiagnosticListener.AllListeners.Subscribe(listener =>
                {
                    if (listener.Name == "Web")
                    {
                        listener.SubscribeWithAdapter(new DiagnosticCollector());
                    }
                });
            }

            public async Task InvokeAsync(HttpContext context)
            {
                await _next.Invoke(context);
            }
        }
    }
}
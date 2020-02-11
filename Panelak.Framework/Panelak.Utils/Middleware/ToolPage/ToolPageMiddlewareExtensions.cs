namespace Panelak.Utils
{

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    public static class ToolPageMiddlewareExtensions
    {
        private static string IndexResponse { get; set; } = null;

        public static IApplicationBuilder UsePanelakUtilsStaticFiles(this IApplicationBuilder app, string toolPagePath = "/Panelak.Utils")
        {
            Dictionary<string, ToolPageMetadataItem> metadata = JsonSerializer.Deserialize<Dictionary<string, ToolPageMetadataItem>>(Panelak.Utils.Properties.Resources.staticfiles_metadata);

            return app.Map(toolPagePath, (fxApp) =>
            {
                fxApp.Run(async (context) =>
                {
                    ToolPageMetadataItem metadataDict = metadata["index.html"];
                    string path = context.Request.Path.Value?.Trim('/');
                    if (!String.IsNullOrEmpty(path))
                    {
                        if (metadata.ContainsKey(path))
                        {
                            metadataDict = metadata[path];

                        }
                        else
                        {
                            context.Response.StatusCode = 404;
                            await context.Response.WriteAsync("Resource not found");
                            return;
                        }
                    }

                    string responseString = Properties.Resources.ResourceManager.GetString(metadataDict.ResourceName);

                    if (metadataDict.ResourceName == nameof(Properties.Resources.Index))
                    {
                        if (IndexResponse == null)
                        {
                            string strList = "";
                            foreach (KeyValuePair<string, ToolPageMetadataItem> item in metadata)
                                strList += $"<li><a href=\"{toolPagePath}/{item.Key}\">{item.Key}</a> - {item.Value.Description}</li>" + Environment.NewLine;

                            IndexResponse = responseString.Replace("{{LIST}}", strList);
                        }
                        responseString = IndexResponse;
                    }

                    context.Response.StatusCode = 200;
                    context.Response.Headers["Content-type"] = metadataDict.ContentType + ";charset=utf-8";
                    await context.Response.WriteAsync(responseString);
                });

            });
        }
    }
}

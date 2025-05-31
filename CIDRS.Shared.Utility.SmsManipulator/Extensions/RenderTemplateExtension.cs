using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Shared.Utility.SmsManipulator.Extensions
{
    /// <summary>
    /// The class render templete
    /// </summary>
    public static class RenderTemplateExtension
    {
        /// <summary>
        /// The method of render templete async
        /// </summary>
        /// <param name="viewPath"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<string> Render(this string viewPath, object[] model)
        {
            var template = string.Empty;


            using (var sr = new StreamReader(viewPath, Encoding.UTF8))
            {
                var content = await sr.ReadToEndAsync(); // Read content 
                template = string.Format(content, model);// Set templete content and model
            }

            return template;
        }
    }
}

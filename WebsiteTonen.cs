using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mastermind
{
    public static class WebsiteTonen
    {
        public static void ToonSite()
        {
            WebClient webClient = new WebClient();
            string tekstVanWebsite = webClient.DownloadString("https://nl.wikipedia.org/wiki/Mastermind");
            Console.WriteLine(ToonTekstVanHtml(tekstVanWebsite));
        }
        //Bron: https://www.mercator.eu/mercator/std/info_aruba/reporting-hoe-gegevens-afdrukken-met-html-tags.html
        private static string ToonTekstVanHtml(string htmlString)
        {
            string htmlTagPattern = "<.*?>";
            var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            htmlString = regexCss.Replace(htmlString, string.Empty);
            htmlString = Regex.Replace(htmlString, htmlTagPattern, string.Empty);
            htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
            htmlString = htmlString.Replace("&nbsp;", " ");
            return htmlString;
        }
    }
}

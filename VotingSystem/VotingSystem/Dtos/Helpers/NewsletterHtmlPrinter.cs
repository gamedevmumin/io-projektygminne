using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.Models;

namespace VotingSystem.Dtos.Helpers
{

    public class NewsletterHtmlPrinter
    {
        public string Print(Newsletter newsletter)
        {

            var html =
                 "<div style=\"font-family: 'Trebuchet MS', sans-serif;\">" +
                $"<br/><br/>" +
                $"<h2>A new edition for { newsletter.EditionDescription.District } has started!</h2>" +
                $"<p>{ newsletter.EditionDescription.Description }</p>" +
                 "<hr/>" +
                 "<div style=\"display: flex; flex-direction: row; \">" +
                 "<ul style=\"list-style-type: none;\">";


            foreach (var projectDescr in newsletter.ProjectDescriptions)
            {
                html +=
                   $"<li><h4>{ projectDescr.Title }</h4>" +
                   $"<p style=\"max-width: 800px; word-wrap: break-word;\">{ projectDescr.Body }</p><hr/></li>";
            }
            html += "</ul></div></div>";

            return html;
        }
    }
}

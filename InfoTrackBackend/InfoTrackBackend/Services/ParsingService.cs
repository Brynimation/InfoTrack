using InfoTrackBackend.Contracts;
using System.Net;
using System.Text.RegularExpressions;

namespace InfoTrackBackend.Services;

public class ParsingService : IParsingService
{
    #region Public Methods

    public List<SolicitorResultsResponseDto> Parse(string input) 
    {
        var resultsList = ExtractResultsList(input);
        var solicitors = GetAllSolicitorInformation(resultsList).Select(stringRep => GetStructuredSolicitorData(stringRep)).ToList();
        return solicitors;
    }

    #endregion

    #region Private Members 

    private (int endIndex, string solicitor) ExtractSolicitorInformation(string input) 
    {
        var match = _resultsItemRegex.Match(input);
        if (!match.Success)
            return (-1, string.Empty);
        return ExtractContentBetweenOpenAndClosingDivs(input, match.Index);
    }

    private List<string> GetAllSolicitorInformation(string input) 
    {
        List<string> solicitorResults = new List<string>();
        var textLeftToScan = input;
        while (!string.IsNullOrWhiteSpace(textLeftToScan)) 
        {
            (int endIndex, string solicitor) = ExtractSolicitorInformation(textLeftToScan);
            if (endIndex == -1) 
                break;
            solicitorResults.Add(solicitor);
            textLeftToScan = textLeftToScan.Substring(endIndex);
        }
        return solicitorResults;
    }

    private (int endIndex, string content) ExtractContentBetweenOpenAndClosingDivs(string input, int startIndex) 
    {
        var firstMatch = _openingDivRegex.Match(input, startIndex);
        int divDepth = 1;
        int curIndex = firstMatch.Index + firstMatch.Length;


        while (curIndex < input.Length)
        {
            var nextOpen = _openingDivRegex.Match(input, curIndex);
            var nextClose = _closingDivRegex.Match(input, curIndex);

            //if no more matches then leave the loop
            if (!nextOpen.Success && !nextClose.Success)
                break;

            //if we successfully see an opening div and that happens before a closing div
            if (nextOpen.Success && (!nextClose.Success || nextOpen.Index < nextClose.Index))
            {
                divDepth++; //so push to the counter stack
                curIndex = nextOpen.Index + nextOpen.Length;//shift the index by the length of the current match
            }
            else //otherwise we've found a closing div
            {
                divDepth--; //so pop from the counter stack

                curIndex = nextClose.Index + nextClose.Length; //continue search by incrementing the current index
                if (divDepth == 0)
                    break;
            }
        }
        return (curIndex, input.Substring(startIndex, curIndex - startIndex));
    }

    private string ExtractResultsList(string input)
    {
        int startIndex = input.IndexOf(_resultsSection, StringComparison.OrdinalIgnoreCase);

        if (startIndex == -1)
            return string.Empty;

        return ExtractContentBetweenOpenAndClosingDivs(input, startIndex).Item2;
    }

    private SolicitorResultsResponseDto GetStructuredSolicitorData(string solicitorSource)
    {
        SolicitorResultsResponseDto dto = new();
        //matches any string of the following form: "<span class = "${any-class-name}<"
        var nameRegex =  new Regex(@"<\s*span\b[^>]*class\s*=\s*""[^""]*""[^>]*>(.*?)<", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        var nameMatch = nameRegex.Match(solicitorSource);
        dto.Name = nameMatch.Success ? Clean(nameMatch.Groups[1].Value) : "N/A";

        //matches any string of the following form: "tel:${anystring}"
        var phoneRegex = new Regex(@"tel:([^""]*)");
        var phoneMatch = phoneRegex.Match(solicitorSource);
        dto.PhoneNumber = phoneMatch.Success ? Clean(phoneMatch.Groups[1].Value) : "N/A";

        //matches any string with the following form:  <address>${any-string}</address>
        var addressRegex = new Regex(@"<address>(.*?)</address>");
        var addressMatch = addressRegex.Match(solicitorSource);
        dto.Address = addressMatch.Success ? Clean(addressMatch.Groups[1].Value) : "N/A";

        return dto;
    }

    private static string Clean(string input) 
    {
        return WebUtility.HtmlDecode(input).Trim();
    }

    private const string _resultsSection = "<div class=\"result-section\">";
    private readonly Regex _resultsItemRegex = new(@"<div\s+class=""result-item[^""]*""\s*>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private Regex _openingDivRegex = new Regex("<div\\b");
    private Regex _closingDivRegex = new Regex("</div>");

    #endregion
}


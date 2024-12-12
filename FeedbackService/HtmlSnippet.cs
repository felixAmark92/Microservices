namespace FeedbackService;

public static class HtmlSnippet
{
    public static string Get() => @"
<h1>Gateway</h1>
<h3>/api/feedback-service/hello</h3>
<p>ger oss ett hej från feedback service</p></br>         
<h3>/api/feedback-service/hello-to-b</h3>
<p>skickar ett message från feedback service.  log för att se medelandet</p></br>
<h3>/api/feedback-service</h3>
<p>ger oss ett hej från feedback service</p></br>         
";

}
namespace ServiceA;

public static class HtmlSnippet
{
    public static string Get() => @"
<h1>Gateway</h1>
<h3>/api/service-a/hello</h3>
<p>ger oss ett hej från service a</p></br>         
<h3>/api/service-a/hello-to-b</h3>
<p>skickar ett message från service a till service b. gå in på service b log för att se medelandet</p></br>
<h3>/api/service-b</h3>
<p>ger oss ett hej från service b</p></br>         
";

}
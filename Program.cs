using PuppeteerSharp;

//string url = "https://listado.mercadolibre.com.do/laptops";
string url = "https://www.amazon.com/s?k=keyboard+65%25&crid=I781GSDXAIHX&sprefix=keyboard+65%25%2Caps%2C114&ref=nb_sb_noss_1";
string chrome = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

//using var browserFetcher = new BrowserFetcher();
//await browserFetcher.DownloadAsync();

await using var browser = await Puppeteer.LaunchAsync(
        new LaunchOptions
        {
            Headless = true,
            ExecutablePath = chrome,
        }
    );

await using var page = await browser.NewPageAsync();

await page.GoToAsync(url);

List<string> keyboards = new List<string>();

//a-size-medium a-color-base a-text-normal
var result = await page.EvaluateFunctionAsync("()=>{" +
    "const a = document.querySelectorAll('.a-size-medium');" +
    //"const a = document.querySelectorAll('.ui-search-item__title');" +
    "const res =[];" +
    "for(let i=0; i<a.length; i++)" +
    "   res.push(a[i].innerText);" +
    "return res;" +
    "}");

foreach (var item in result)
{
    keyboards.Add(item.ToString());
}

foreach(var keyboard in keyboards)
    Console.WriteLine(keyboard);


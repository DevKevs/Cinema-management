// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace Movie_app.Client.Pages.Movies
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\_Imports.razor"
using Movie_app.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\_Imports.razor"
using Movie_app.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\_Imports.razor"
using Radzen;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\_Imports.razor"
using Radzen.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\Pages\Movies\MovieView.razor"
using CsvHelper;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\Pages\Movies\MovieView.razor"
using Movie_app.Client.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\Pages\Movies\MovieView.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\Pages\Movies\MovieView.razor"
using System.IO;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\Pages\Movies\MovieView.razor"
using System.Globalization;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/MovieView")]
    public partial class MovieView : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 273 "C:\Users\Kevin Feliz\Desktop\Movie_app_blazor\Movie_app\Client\Pages\Movies\MovieView.razor"
       
    RadzenDataGrid<Movie> GridMovie;
    List<Movie> Movies;
    List<Movie> CsvData = new List<Movie>();
    bool IsValid = false;
    Movie _Movie = new Movie();
    DateTime? value = DateTime.Now;
    protected override async Task OnInitializedAsync()
    {
        Movies = await Http.GetFromJsonAsync<List<Movie>>("https://localhost:7297/api/Movies");
    }
    void OnChange()
    {
        _Movie.ReleaseDate = value.Value;
    }
    void fillData(Movie movie)
    {
        _Movie = movie;
    }
    async Task  HandleFileSelected(InputFileChangeEventArgs e)
    {
        IBrowserFile imgFile = e.File;
        var buffers = new byte[imgFile.Size];
        await imgFile.OpenReadStream().ReadAsync(buffers);
        string imageType = imgFile.ContentType;
        _Movie.Photo = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";

    }
    async Task SendMovie()
    {
        try
        {
            string json = JsonConvert.SerializeObject(_Movie);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var responses = await Http.PostAsync("https://localhost:7297/api/Movies", httpContent);
            await Reload();
            _Movie = new Movie();
            IsValid = true;
        }
        catch
        {
            IsValid = false;
        }
    }
    async Task EditMovie()
    {
        try
        {
            string json = JsonConvert.SerializeObject(_Movie);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var responses = await Http.PutAsync("https://localhost:7297/api/Movies", httpContent);
            await Reload();
            IsValid = true;
        }
        catch
        {
            IsValid = false;
        }
    }
    async Task DeleteMovie()
    {
        try
        {
            Movies.Remove(_Movie);
            var responses = await Http.DeleteAsync($"https://localhost:7297/api/Movies/{_Movie.Id}");
            await Reload();
            IsValid = true;
        }
        catch
        {
            IsValid = false;
        }
    }
    async Task Reload()
    {
        Movies = await Http.GetFromJsonAsync<List<Movie>>("https://localhost:7297/api/Movies");
        await GridMovie.Reload();
    }
    private void ExportarCSV()
    {
        CsvData.Clear();
        foreach (var i in Movies)
        {
                CsvData.Add(i);
        }

            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream))
                {
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(CsvData);
                    }

                    var arr = memoryStream.ToArray();
                    JSRuntime.InvokeAsync<object>("saveAsFile",
                    "peliculas.csv",
                    Convert.ToBase64String(arr));
                }
            }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
    }
}
#pragma warning restore 1591
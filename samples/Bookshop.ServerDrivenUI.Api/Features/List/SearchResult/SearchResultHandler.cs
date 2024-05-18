using Bolt.Endeavor;
using Bolt.IocScanner.Attributes;
using Bolt.ServerDrivenUI;
using Bolt.ServerDrivenUI.Core;
using Bolt.ServerDrivenUI.Core.Elements;
using Bolt.ServerDrivenUI.Extensions.Web.RazorParser;
using Bookshop.ServerDrivenUI.Api.Features.Shared;
using Bookshop.ServerDrivenUI.Api.Features.Shared.Repositories;

namespace Bookshop.ServerDrivenUI.Api.Features.List.SearchResult;

[AutoBind]
internal sealed class SearchResultHandler(IRazorXmlViewParser parser,
    IAppUrlBuilder appUrlBuilder,
    IBookRepository bookRepository) 
    : ScreenElementProvider<ListPageRequest>
{
    protected override SectionInfo ForSection => new()
    {
        Name = "search-result"
    };
    
    protected override async Task<MaySucceed<IElement>> Get(
        IRequestContextReader context, 
        ListPageRequest request, 
        CancellationToken ct)
    {
        var books = await bookRepository.GetAll();

        var items = new List<SearchItemViewModel[]>();

        var groups = new List<SearchItemViewModel>();
        
        foreach (var record in books)
        {
            groups.Add(new SearchItemViewModel
            {
                Title = record.Title,
                ImageUrl = record.ImageUrl,
                DetailsUrl = appUrlBuilder.Details(record.Isbn, record.Title)
            });

            if (groups.Count == 2)
            {
                items.Add(groups.ToArray());
                groups.Clear();
            }
        }

        if (groups.Count > 0)
        {
            items.Add(groups.ToArray());
        }

        return await parser.Read(new RazorViewParseRequest<SearchResultViewModel>
        {
            ViewModel = new SearchResultViewModel
            {
                Heading = "Books",
                ItemsGroup = items
            }
        });
    }
}
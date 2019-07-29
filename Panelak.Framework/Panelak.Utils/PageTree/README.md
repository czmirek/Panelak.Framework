# Panelak.Utils page tree

Convention based page/menu tree builder for ASP.NET Core MVC.

Usage:

- Install `Panelak.Utils` nuget
- Add this to your Startup.cs 

```csharp
services.AddPageTree(typeof(Startup).Assembly);
```


## Building the page tree
Mark your actions with the `[Route]` attribute like in this example.

```csharp
[Route("/")]
public IActionResult MainPage() => View();

[Route("/page1")]
public IActionResult Page1() => View();

[Route("/page1")]
public IActionResult Page2() => View();

[Route("/page1/detail/{id}")]
public IActionResult Page1Detail(int id) => View();

[Route("/page2/detail/{id}")]
public IActionResult Page2Detail(int id) => View();

[Route("/page2/something")]
public IActionResult Page2DetailSomething(int id) => View();

[Route("/page2/something/else")]
public IActionResult Page2DetailSomethingElse(int id) => View();
```

The `IPageTreeProvider` now contains the tree with this structure.

- `/`
  - `/page1`
    - `/page1/detail/{id}`
  - `/page2`
    - `/page2/detail/{id}`
    - `/page2/something`
      - `/page2/something/else`

Only the Route attribute template is considered, the actions 
method signatures are provided only for clarity.

The `IPageTreeProvider` is in a singleton scope and you can 
inject it anywhere where you need to render menus and such.

## Getting the current page

Optionally mark your actions with either the `Display` 
or the custom made `PageMetadataAttribute`.

```csharp
[Route("/")]
[Display(ResourceType = typeof(Resources.Pages), Name = "HomeCaption")]
public IActionResult Index() => View();
```

or

```csharp
[Route("/")]
[PageMetadata(ResourceType = typeof(Resources.Pages), Caption = "HomeCaption", Header = "HomeHeader")]
public IActionResult Index() => View();
```

Now use the `ICurrentPageProvider`...

### ... to read the current page metadata.

```html
@inject ICurrentPageProvider currentPage

<h1>@currentPage.CurrentPage?.Header</h1>
```

### ... to generate a "Go back" button

```html
@inject ICurrentPageProvider currentPage

<a href="@currentPage.CurrentPageTree.Parent.Page.CurrentUrl">
    Go back to @currentPage.CurrentPageTree.Parent.Page.Caption
</a>
```

### ... to build breadcrumbs with automatically generated working links!
```html
@inject ICurrentPageProvider currentPage
<nav>
    <ol class="breadcrumb">
        @for (int i = 0; i < currentPage.CurrentPagePath.Count; i++)
        {
            if(i == currentPage.CurrentPagePath.Count - 1)
            {
                <li class="breadcrumb-item active">@(currentPage.CurrentPagePath[i].Caption)</li>
            }
            else
            {
                <li class="breadcrumb-item"><a href="@(currentPage.CurrentPagePath[i].CurrentUrl)">@(currentPage.CurrentPagePath[i].Caption)</a></li>
            }
        }
    </ol>
</nav>
```
# Panelak.Utils page tree

Convention based page/menu tree builder for ASP.NET Core MVC.

Usage:

- Install `Panelak.Utils` nuget
- Add this to your Startup.cs 

```csharp
services.AddPageTree(typeof(Startup).Assembly);
```

- Mark your actions with the `[Route]` attribute like in this example.

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

- Use the `ICurrentPageProvider` from the scoped context 
  to identify the current action or the current node of the
  page tree to build breadcrumbs, "Go back" buttons or 
  other navigational UI stuff.



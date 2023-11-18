# TimerRepro
.NET 8 Blazor components unexpected behaviour
## Entry point
- Two the same interactive components placed in the same place on two different static pages
## Observed behaviour
- The same component instance is render on both pages

https://github.com/gaplin/TimerRepro/assets/50521366/bcc4099b-a557-4481-bd39-2fdccf62011c

## Expected behaviour
- Two different instances are rendered

https://github.com/gaplin/TimerRepro/assets/50521366/06d6673f-e798-4cee-be98-638c3628c9b4

## Possible workarounds
- Using @<!-- -->key attribute directly on components
- Adding empty component **before** interactive one on either counter or home page.
- Using InteractiveMode directly on page

## Why is this an issue
- Documentation doesn't have a word about such behaviour
- Hidden dependency between pages \
  Let's assume new static page was added to project.
  ```razor
  @page "/newPage"
  <TimerExample @rendermode=InteractiveServer/>
  @*
  Some stuff
  *@
  ```
  You can't determine what's gonna happen when you navigate to this page without knowledge about other pages because:
  - If there is other page with matching layout until 'Some stuff' then when navigating only between these 2, the timer would be the same.
  - If there is no page with matching layout, then new component will be rendered no matter from which page you come to this page
  
  


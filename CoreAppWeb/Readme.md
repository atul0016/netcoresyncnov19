1. Tag Helper
- HTML Pre -Parsed Attribute system in ASP.NET Core
- they are used for Model binding from Server-to-Client for HTML elements
- e.g.	
	asp-action --> Used to generate Http Request for Action Method for MVC Controller
	asp-controller --> Http Request for MVC COntroller
	asp-for --> Model Binding of Primptive type property from Model class
	asp-items --> To Load IEnumerable on View for Rendering with Model Binding
	asp-route
	asp-route-id
2. Identity Scafolder
	- Microsoft.AspNetCore.Identity
		- IdentityUser
			- User Metadata
		- IdentityRole
			- Role Metadata
		- UserManager<IdentityUser>
			- Used to Manage Application Users
		- RoleManager<IdentityRole>
			- Used to Manage Application Roles
			- Define Users for Roles
		- SignInManager
			- Used to Manage Application Login Process using IdentityUser and IdentityRole 
	- Microsoft.AspNetCore.Identity.EntityFramework
		- IdentityDbContext
			- Genetrates ASP.NET Security Database Tables 
			- Provides Mechanism of Password Hash
			- Manages UsersInRole
			- Manage User/Role Claims
3. The 'PageModel' class is the Base class for WebForms in ASP.NET Core
	- This also provide the Model bindiong linke MVC to Views.
	- This also provide the Model bindiong linke MVC to Views.
# 🌺 Flower Shop Sales Tracker
A sales tracking application built for my parents’ business that helps keep track of products leaving the warehouses. It was created to solve a real problem in their day-to-day work, while also giving me the opportunity to learn by building something practical from start to finish.

## 💻 Technologies
##### Frontend
- `Vue.js`
- `Tailwind CSS`
- `Axios`
- `Pinia`
##### Backend
- `ASP.NET Core`
- `Entity Framework Core`
- `JWT Authentication`
##### Database
- `Microsoft SQL Server`
## ✨ Features
##### Administrator capabilities
- Manage warehouses and organize products within them
- Register flower sales efficiently
- View historical sales data by day or month
- Create, edit, and manage sales records
##### User capabilities
- Register daily sales through a simplified interface
## ⏳ The process
I began by designing the solution in PenPot (a Figma alternative), mapping out an idea to replace manual, paper-based sales tracking.

Next, I set up the foundational database using Entity Framework.

From there, I developed the application incrementally using a slice-based approach, building each feature end-to-end across the frontend, backend, and data models.

1. The first module I implemented was authentication and account creation.
2. The next module I implemented was warehouse management, where warehouses can be created and products can be linked to them.
3. After that, I added the sales creation workflow, where users can register sales made from the created warehouses.
4. Finally, I built the sales history and management section, allowing users to view and edit recorded transactions.

During this process, I prioritized clean structure of the project and actual understanding of the concepts that I was working with.

## 📚 What I learned
During this project, I picked up important skills and a better understanding of complex ideas, which improved my logical thinking.
##### RESTful APIs and Endpoints
- I learned how to design and work with RESTful APIs and endpoints, and how they act as the main communication layer between the frontend and backend of the application
##### Entity Framework Core
- **Ease of use**: I learned how to use Entity Framework Core to create and manage my database with a code first approach
- **Security**: I also learned this improves the security of my application by taking care of threats like SQL Injection
##### Authentication & Authorization
- To securely store user credentials and implement role-based permissions, I learned how to use ASP.NET Core Identity within the application
- I also explored JWT authentication and how token-based login systems can be used to securely authenticate users and protect API endpoints
##### AutoMapper
- I used AutoMapper to map the DTOs to database models in an efficient way
##### Environment Variables & User Secrets
- This project helped me become familiar with these concepts, which are commonly used in production applications.
##### Vue.js
- Through this project, I explored the framework and learned how to use it to materialize my ideas

## 💭 What I can improve
- Add statistics functionality so the user can have insights on how the business is going
- Allow reordering of warehouses and products
- A more professional looking UI
- Ability to export data into Excel files
- Multi-language support

## 🎥 Video
Here is a video demonstrating the functionality of the app.
The video shows the process of 
- registering a regular account
- using that account to register a sale for today
- logging out of that account and logging into the admin account
- using the admin interface to:
  - create a warehouse
  - link products to that warehouse
  - register a sale for today
  - visit the sales history
  - add an entry in the history
  - modifying/deleting an entry in the history

The UI is in Romanian as the app was made to be used primarily by Romanian-speaking users.
The app is intended to be used on mobile devices for registering sales and managing warehouses, and on desktop for reviewing sales history.


https://github.com/user-attachments/assets/5b170433-7714-469a-b730-9e218b28d889




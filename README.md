# FurnitureStore
This is a graduation project for C# Web Course in SoftUni, created on .NET CORE.

# How App Works
For communication with the Database I have used Repository/IRepository service/interface given by Stamo Petkov (our lector).
As Database I have used Microsoft SQL.

# Functionality
App is a web shop for Day room furnitures and has 5 entity models - Sofa, Armchair, Table, Chair, TV-Table. It has the following pages:
-Home Page: Welcomes the visitor;
-Login Page: Default Login page. Registered Users can login;
-Register Page: Default Register page. Not registered users can register;
-Catalog Page: List of Available item categories. Accessible for not registered users;
-Category Catalog Page: List of all Available items from one category (ex: All Chairs available). Accessible for not registered users, but no any action available. Registered users, which are not creator of the offer can buy (buying of item is not implemented yet) item, or see details about the iitems. Registered users, which are creator of the offer/item cannot see the Buy button;
-Item Details Page: Detailed View of the item. Only registered users have access to this page. If user is not the Creator can Buy the item and if user is creator - can Edit and Delete the item;
-Edit Item Page: Edits the item and Redirect to Category Catalog Page;
-Delete Item: changes IsActive flag in Database to false and removes the item from the View;
-Create Item Page: Every registered user can create items;
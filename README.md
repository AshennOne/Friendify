# Friendify

> Simple social media app for meeting new friends\
> Live demo [_here_](https://friendify-xcrl.onrender.com)\
> API documentation [_currently not available_]()\
> API UI via swagger [_here_](https://friendify-xcrl.onrender.com/swagger/index.html)

## Table of Contents

-  [General Info](#general-information)
-  [Technologies and Tools Used](#technologies-and-tools-used)
-  [Features](#features)
-  [Screenshots](#screenshots)
-  [Project Status](#project-status)
-  [To do/ Improvement Ideas](#to-do--improvement-ideas)
-  [Acknowledgements](#acknowledgements)
-  [Setup and install](#setup-and-install)

## General Information

This project was made by me, without any tutorials and guides. It's the biggest project i've ever made. In a nutshell it's social media app with some CRUD operations, with auth system and UI.

## Technologies and Tools Used

-  Frontend: Angular JS (and Typescript) - version 14
-  Backend (API): ASP .NET - version 7.0
-  Containers: Docker desktop - version 4.20
-  Database: PostgreSQL
-  Unit test framework: XUnit
-  Email sending - google SMTP
-  Styles: Bootstrap 5 + Bootswatch theme
-  Image Cloud: Firebase
-  Frontent Libs: Angular Material, Ngx Bootstrap
-  ORM (Object-Relational Mapping): Entity framework 7.0
-  API Testing: Postman
-  API UI - Swagger UI
-  Version Control - Git
-  Package manager - NuGet and npm
-  Host: Render.com

## Features

-  Login and register form with validation (API and client side)
-  Required email verification with SMTP
-  Password reset
-  Jwt Token with claims
-  CRUD operations for posts
-  CRUD operations for messages
-  CRUD operations for users
-  CRUD operations for likes
-  CRUD operations for notifications
-  Notifications when someone did any interaction with your profile
-  "is online" icon using signalR
-  Reposting posts of others
-  Profile picture set-up/change
-  Searchbar
-  Image Upload and cropping
-  Guards - client side security system preventing unwanted behaviors
-  Interceptor - client side system to sending auth token
-  Loading screen

## Screenshots

![Main Page](https://res.cloudinary.com/dwy4hhhjr/image/upload/w_900,h_495/v1707938494/screen1_djjirv.png)
![Creating New Post](https://res.cloudinary.com/dwy4hhhjr/image/upload/w_900,h_495/v1707938494/screen3_if27o2.png)
![Notifications Page](https://res.cloudinary.com/dwy4hhhjr/image/upload/w_900,h_495/v1707938493/screen4_elfpek.png)
![Last Conversations](https://res.cloudinary.com/dwy4hhhjr/image/upload/w_900,h_495/v1707938493/screen5_ep8nyc.png)
![Messages](https://res.cloudinary.com/dwy4hhhjr/image/upload/w_900,h_495/v1707938494/screen6_q2btyl.png)
![Profile](https://res.cloudinary.com/dwy4hhhjr/image/upload/w_900,h_495/v1707938493/screen7_czh0s8.png)

## Project Status

The project is done, but minor changes and implementing new features is possible in the future

## To do / Improvement Ideas

-  Better caching system
-  Query optimization
-  Client side optimization
-  Pagination
-  Creating Message Hub And notification Hub (SignalR) for real time interactions.

## Acknowledgements

-  I'm not an author of photos included in project, but they're open source

## Setup and install

**Docker instalation**

1. Make sure you have docker dektop installed
2. Download latest release from github
3. Run Docker dektop
4. Run file rundocker.bat

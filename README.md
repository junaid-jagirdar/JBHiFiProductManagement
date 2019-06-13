# JBHiFiProductManagement
 
E-commerce and retail is a dynamic market. There is a tremendous of data transactions happening behind the system. In line with this business scenario I have designed the programming challenge using <b>CQRS architecture pattern</b> which is very well suited for heavy data using applications.

Technology Used
1.Back End: <b>Asp.Net core 2.o Web API</b>
2.Front End: Angular 7
3.Database: <b>LiteDB </b>(In memory Persistent Database which could be plugged in with any Database engines).
4.Unit Testing:<b> Fluent assertions</b> (BDD style of unit testing. Easier to understand business’s language)
5.IOC container : <b>Structure Map</b>.(.net core has built in ,it is still maturing 😊)
6.Authentication: I have used <b>JWT bearer token </b> authentication.
Other tools: <b>Swagger </b>has been set up to have a integrated.

Steps to Set up and Run:
I have separated out the client and server.
The Web API ‘s and angular apps are separated.
You can host the WEB API’s  on a APP server and UI on a web server.

For development scenario
1.	Run the Web API project on Visual studio which will have the WEB API s hosted.
2.	Take the url of the hosted Web API’s and copy to the environment file in the angular app.
3.	Run the angular app.
4.	Login page appears
5.	Login using credentials.
6.	On Products page  you can add, delete and update products.

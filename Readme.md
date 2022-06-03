<div id="top"></div>


<!-- PROJECT LOGO -->
<br />
<div align="center">


<h1 align="center">Sámuel Léránt's Home Project</h1>

  <p align="center">
    This is my Programming 3 - Programming 4
    <br />
    Home Project
    <br />
    <br />
    <a href="https://github.com/samu112/AKRYTN_HFT_2021221/blob/master/Database.png"><strong>Database Structure »</strong></a>
    <br />
    <br />
    Frontend:
    <br />
    <a href="https://github.com/samu112/AKRYTN_HFT_2021221/tree/master/AKRYTN_HFT_2021221.Client">Console App</a>
    ·
    <a href="https://github.com/samu112/AKRYTN_HFT_2021221/tree/master/WpfClient">WPF App</a>
    ·
    <a href="https://github.com/samu112/AKRYTN_HFT_2021221/tree/master/AKRYTN_HFT-2021221.JSClient">Js Client</a>
    <br />
    Shared:
    <br />
    <a href="https://github.com/samu112/AKRYTN_HFT_2021221/tree/master/AKRYTN_HFT_2021221.Models">Models</a>
    <br />
    Backend:
    <br />
    <a href="https://github.com/samu112/AKRYTN_HFT_2021221/tree/master/AKRYTN_HFT_2021221.Data">Data Layer</a>
    ·
    <a href="https://github.com/samu112/AKRYTN_HFT_2021221/tree/master/AKRYTN_HFT_2021221.Repository">Repository Layer</a>
    ·
    <a href="https://github.com/samu112/AKRYTN_HFT_2021221/tree/master/AKRYTN_HFT_2021221.Logic">Logic Layer</a>
    <br />
    <a href="https://github.com/samu112/AKRYTN_HFT_2021221/tree/master/AKRYTN_HFT_2021221.Endpoint">Endpoint</a>
    ·
    <a href="https://github.com/samu112/AKRYTN_HFT_2021221/tree/master/AKRYTN_HFT_2021221.Test">Unit Tests</a>



  </p>
  </br>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#used-nuget-packages">Used Nuget Packages</a></li>
        <li><a href="#used-project-types">Used Project Types</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
      </ul>
    </li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

This was my home project for programming 3 and programming 4 at Obuda University.
</br></br>
For programming 3 I had to make a layered Console Application which had to use the given API calls(PUT, POST, GET) to interact with a database and had to create some non-CRUD method too.
</br>
(that is why I used PUT instead of UPDATE to update the data).
</br>
><strong>Layers:</strong> Data, Repository, Logic/Business, Client/Presentation

I also had to create some Unit Tests to test the logic methods.
</br></br>
For programming 4 I had to create a GUI with WPF that could do everything that the console Application could and also a Web Interface where a user could use CRUD operations.

<p align="right">(<a href="#top">back to top</a>)</p>



### Used Nuget Packages:

* JSClient: 
  * [AutoMapper](https://www.nuget.org/packages/automapper/)
* WpfClient:
  * [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)
  * [Microsoft.Toolkit.Mvvm](https://www.nuget.org/packages/Microsoft.Toolkit.Mvvm/)
* Client:
  * [ConsoleMenu-simple](https://www.nuget.org/packages/ConsoleMenu-simple/)
  * [Microsoft.AspNet.WebApi.Client](https://www.nuget.org/packages/Microsoft.AspNet.WebApi.Client/)
* Data:
  * [Microsoft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/)
  * [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/)
  * [Microsoft.EntityFrameworkCore.Proxies](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Proxies/)
  * [Microsoft.EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools)
* Endpoint:
  * [Swashbuckle.AspNetCore](https://www.nuget.org/packages/Swashbuckle.AspNetCore/)
* Test:
  * [Microsoft.NET.Test.Sdk](https://www.nuget.org/packages/Microsoft.NET.Test.SDK)
  * [NUnit](https://www.nuget.org/packages/NUnit/)
  * [NUnit3TestAdapter](https://www.nuget.org/packages/NUnit3TestAdapter/)
  * [Moq](https://www.nuget.org/packages/moq/)

<p align="right">(<a href="#top">back to top</a>)</p>



### Used Project Types:
* Console Application (.NET Core) - .NET 5
* WPF Application (.NET Core) - .NET 5
* ASP.NET Core Web App - .NET 5
* Class Library - .NET 5

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started

1. Clone or Download the Repository.
2. Open the "AKRYTN_HFT_2021221.sln" file in [Visual Studio](https://visualstudio.microsoft.com/)
3. In the "Solution Explorer" window right click on the solution and click on "Set Startup Projects..."
4. Tick "Multiple Startup Projects"
5. Set "Endpoint" to "Start" with any other other client (Client, WPF, JSClient) you want to see, then press OK
6. Press F5 to start the projects
7. Wait until the Endpoint loads(it will open the browser), then you can use the clients



### Prerequisites

You need [Visual Studio 2019 version 16.9.2 or a later version](https://visualstudio.microsoft.com/downloads/?utm_medium=microsoft&utm_source=docs.microsoft.com&utm_campaign=inline+link&utm_content=download+vs2019) with the [.NET Core cross-platform](https://docs.microsoft.com/en-us/archive/msdn-magazine/2016/april/net-core-net-goes-cross-platform-with-net-core) development workload installed. The .NET 5.0 SDK is automatically installed when you select this workload.


<p align="right">(<a href="#top">back to top</a>)</p>


<!-- CONTACT -->
## Contact

Sámuel Léránt - lerantsamuel@gmail.com

Project Link: [https://github.com/samu112/AKRYTN_HFT_2021221](https://github.com/samu112/AKRYTN_HFT_2021221)

<p align="right">(<a href="#top">back to top</a>)</p>




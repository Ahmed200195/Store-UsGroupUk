<%@ Page Title="Home" Language="C#" MasterPageFile="~/Client/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Store.Client.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- start in carsoule header -->
    <div class="contaier px-8 mx-auto lg:mt-0 mt-4" style="margin-top: 135px">
      <div class="carousel-wrapper w-full h-full overflow-hidden relative">
        <!-- Slides -->
        <asp:Literal ID="ltImgSlider" runat="server"></asp:Literal>
        <!-- Next and Previous Buttons -->
        <button class="btn previous cursor-pointer rounded-lg py-2 px-3 m-3 absolute top-2/4 left-0 border-none"
          data-button="previous">
          <i class="fa-solid fa-angle-left"></i>
        </button>
        <button class="btn next cursor-pointer rounded-lg py-2 px-3 m-3 absolute top-2/4 right-0 border-none"
          data-button="next">
          <i class="fa-solid fa-chevron-right"></i>
        </button>
      </div>
    </div>
    <!-- end in carsoule header -->

    <!-- start in category -->

    <div class="container mx-auto px-8 mt-10">
        <h1 class="titleMain text-center mb-5">Shop By Category</h1>
        <div class="main-carousel carouselCategory">
            <!-- carsoule slides -->
            <asp:Literal ID="ltCategory" runat="server"></asp:Literal>
        </div>
    </div>
    <!-- end in category -->

    <!-- start in featured product -->
    <section class="bg-gray-200 py-10 mt-5">
        <h1 class="titleMain text-center ">featured product </h1>
        <div class="container px-4 mx-auto">
            <div
                class="product-card-container pt-4 grid lg:grid-cols-4 md:grid-cols-3 sm:grid-cols-2 grid-cols-1 justify-start items-center gap-4 transition-all delay-150 ease-in">

                <!-- 1 -->
                <asp:Literal ID="ltFeaturedProduct" runat="server"></asp:Literal>
                

            </div>
        </div>
    </section>
    <!-- end in featured product -->

    <!-- start in  product bugs -->

    <asp:Literal ID="ltProdcutsByDept" runat="server"></asp:Literal>

    <!-- end in  product bugs -->
</asp:Content>

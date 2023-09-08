<%@ Page Title="" Language="C#" MasterPageFile="~/Client/Site1.Master" AutoEventWireup="true" CodeBehind="DetailsProduct.aspx.cs" Inherits="Store.Client.DetailsProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main style="margin-top: 135px">
        <article>
            <section class="section product sectionProduct productDetails productElement" aria-label="product" data-id="10">
                <div class="container px-4 mx-auto">
                    <div class="product-slides relative overflow-hidden">
                        <div class="slider-banner flex" data-slider>

                            <asp:Literal ID="ltImgSilder" runat="server"></asp:Literal>

                        </div>

                        <button type="button" class="slide-btn rounded prev absolute p-3 top-2/4 left-4" aria-label="Previous image" data-prev>
                            <i class="fa-solid fa-angle-left"></i>
                        </button>

                        <button type="button" class="slide-btn rounded next absolute p-3 top-2/4 right-4" aria-label="Next image" data-next>
                            <i class="fa-solid fa-angle-right"></i>
                        </button>
                    </div>

                    <div class="product-content">
                        <p class="text-xs product-subtitle">Product name</p>

                        <h1 id="titleProduct" class="text-3xl product-title product-name" runat="server">Fall Limited Edition Sneakers</h1>

                        <p id="InfoProdcut" class="product-text text-[18px]" runat="server">
                            These low-profile sneakers are your perfect casual wear
                companion. Featuring a durable rubber outer sole, they’ll
                withstand everything the weather can offer.
                        </p>

                        <div class="size">
                            <h1 class="text-xl capitalize text-[#3c3b6e] my-4">select size
                            </h1>
                            <div class="checkElement flex gap-4">
                                <asp:Literal ID="ltSizes" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="color">
                            <h1 class="text-xl capitalize text-[#3c3b6e] my-4">color</h1>
                            <asp:DropDownList ID="ddlColors" CssClass="bg-gray-50 border border-gray-300  text-sm rounded-lg text-[#3c3v6e]  block w-full p-2.5  dark:border-gray-600 dark:placeholder-[#3c3b6e] capitalize" runat="server" DataSourceID="sqlColors" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
                            <asp:SqlDataSource ID="sqlColors" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>' SelectCommand="SELECT * FROM [Color] ORDER BY [Id]"></asp:SqlDataSource>
                        </div>

                        <div class="wrapper">
                            <asp:Literal ID="ltPrice" runat="server"></asp:Literal>
                        </div>

                        <div class="btn-group">
                            <div class="counter-wrapper">
                                <button type="button" class="counter-btn" data-qty-minus>
                                    <i class="fa-solid fa-minus"></i>
                                </button>

                                <span class="coountProduct span" data-qty>1</span>

                                <button type="button" class="counter-btn" data-qty-plus>
                                    <i class="fa-solid fa-plus"></i>
                                </button>
                            </div>

                            <button type="button" class="cart-btn add-to-cart">
                                <ion-icon name="bag-handle-outline" aria-hidden="true"></ion-icon>
                                <span class="span">Add to cart</span>
                            </button>
                        </div>
                    </div>
                </div>
            </section>
        </article>
    </main>
    <!-- end in details -->

    <!-- start in  -->
    <section class="container mx-auto px-8 mt-10">
        <div class="grid lg:grid-cols-2 grid-cols-1">
            <!-- element 1 -->
            <figure class="lg:flex justify-center items-center h-3/4 hidden ">
                <img src="../images/detailsProduct/undraw_empty_cart_co35.svg" class="h-full" alt="">
            </figure>
            <!-- element 2 -->
            <div>
                <h1 class="titleMain text-center mb-5">frequently bought together</h1>
                <div class="main-carousel carouselShared">

                    <asp:Literal ID="ltFrequentlyProduct" runat="server"></asp:Literal>

                </div>
            </div>
        </div>
    </section>
    <!-- end in  -->
</asp:Content>

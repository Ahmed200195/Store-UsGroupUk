<%@ Page Title="" Language="C#" MasterPageFile="~/Client/Site1.Master" AutoEventWireup="true" CodeBehind="DetailsProduct.aspx.cs" Inherits="Store.Client.DetailsProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- start in carsoule -->
    <link rel="stylesheet" href="https://unpkg.com/flickity@2/dist/flickity.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <article>
            <section class="section product sectionProduct productDetails productElement" aria-label="product" data-id="<%=Request.QueryString["id"] %>" data-count="<%=Request.QueryString["cnt"] %>" >
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
                        <p class="text-xs product-subtitle"><%= Application["lang"].ToString() == "en" ? "Product name" : "اسم المنتج" %></p>

                        <h1 id="titleProduct" class="text-3xl product-title product-name" runat="server">Fall Limited Edition Sneakers</h1>

                        <p id="InfoProdcut" class="product-text text-[18px]" runat="server">
                            These low-profile sneakers are your perfect casual wear
                companion. Featuring a durable rubber outer sole, they’ll
                withstand everything the weather can offer.
                        </p>

                        <div class="flex justify-between items-center my-3">
                            <h1 class="color text-[#504f85] font-bold capitalize"><%=  Application["lang"].ToString() == "en" ? "Color" : "اللون" %> :<span id="snColor" class="text-gray-400 font-medium" runat="server">red</span>
                            </h1>
                            <h1 class="size text-[#504f85] font-bold capitalize"><%=  Application["lang"].ToString() == "en" ? "Size" : "القياس" %> :<span id="snSize" class="text-gray-400 font-" runat="server">70</span></h1>
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
                                <span class="span"><%=  Application["lang"].ToString() == "en" ? "Add to cart" : "أضف إلى السلة" %></span>
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
                <img src="../images/detailsProduct/undraw_empty_cart_co35.svg" class="h-full" alt=""/>
            </figure>
            <!-- element 2 -->
            <div>
                <h1 class="titleMain text-center mb-5"><%=  Application["lang"].ToString() == "en" ? "frequently bought together" : "اشترى في كثير من الأحيان جنبا إلى جنب" %></h1>
                <div class="main-carousel carouselShared mb-6">

                    <asp:Literal ID="ltFrequentlyProduct" runat="server"></asp:Literal>

                </div>
            </div>
        </div>
    </section>
    <script src="https://unpkg.com/flickity@2/dist/flickity.pkgd.min.js"></script>
    <!-- end in  -->
</asp:Content>

<%@ Page Title="Home" Language="C#" MasterPageFile="~/Client/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Store.Client.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- start in carsoule header -->
    <div class="contaier px-8 mx-auto lg:mt-0 mt-4">
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
                <div
                    class="productElement relative  flex w-full  flex-col overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg transition-all duration-300 hover:shadow-2xl hover:-translate-y-1 ">
                    <a class="relative mx-3 mt-3 flex h-60 overflow-hidden rounded-xl" href="detailsProduct.html">
                        <img class="imgProduct object-fill w-full h-full" src="puplic/images/land_tool/land_1.jpg"
                            alt="product image" />
                        <span
                            class="absolute top-0 left-0 m-2 rounded-full bg-[#b22234] px-2 text-center text-sm font-medium text-white">39%
                OFF</span>
                    </a>
                    <div class="mt-4 px-5 pb-5">
                        <a href="detailsProduct.html">
                            <div>
                                <h5 class="text-xl tracking-tight text-[#504f85] product-name">Nike Air MX Super 2500 - Red</h5>
                            </div>
                            <div class="mt-2 mb-5 flex items-center justify-between">
                                <p>
                                    <span class="text-xl font-normal text-[#504f85] product-price uppercase">kd <span>400</span>
                                    </span>
                                    <span class="salePrice text-sm text-[#504f85] line-through uppercase">kd699</span>
                                </p>

                            </div>
                        </a>
                        <div
                            class="add-to-cart flex gap-2 items-center justify-center rounded-md bg-[#007a3d] cursor-pointer px-5 py-2.5 text-center text-sm font-medium text-white transition-all hover:bg-[#117a3d] focus:outline-none">
                            <i class="fa-solid fa-cart-shopping"></i>
                            Add to cart
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>
    <!-- end in featured product -->

    <!-- start in  product bugs -->
    <section class="py-5 mt-5">
        <div class="container mx-auto px-8">
            <div class="flex justify-between items-center">
                <h1 class="titleMain">tool land </h1>
                <a href="showProduct.html" class="text-[#3c3b6e] text-xl">view all</a>
            </div>
            <div class="product-card-container-outer">

                <div
                    class="product-card-container pt-4 grid lg:grid-cols-4 md:grid-cols-3 sm:grid-cols-2 grid-cols-1 justify-start items-center gap-4 transition-all delay-150 ease-in">
                    <!-- 1 -->
                    <div
                        class="productElement relative  flex w-full  flex-col overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg transition-all duration-300 hover:shadow-2xl hover:-translate-y-1 ">
                        <a class="relative mx-3 mt-3 flex h-60 overflow-hidden rounded-xl" href="detailsProduct.html">
                            <img class="imgProduct object-fill w-full h-full" src="puplic/images/land_tool/land_1.jpg"
                                alt="product image" />
                            <span
                                class="absolute top-0 left-0 m-2 rounded-full bg-[#b22234] px-2 text-center text-sm font-medium text-white">39%
                  OFF</span>
                        </a>
                        <div class="mt-4 px-5 pb-5">
                            <a href="detailsProduct.html">
                                <div>
                                    <h5 class="text-xl tracking-tight text-[#504f85] product-name">Nike Air MX Super 2500 - Red</h5>
                                </div>
                                <div class="mt-2 mb-5 flex items-center justify-between">
                                    <p>
                                        <span class="text-xl font-normal text-[#504f85] product-price uppercase">kd <span>400</span>
                                        </span>
                                        <span class="salePrice text-sm text-[#504f85] line-through uppercase">kd699</span>
                                    </p>

                                </div>
                            </a>
                            <!-- add to cart -->
                            <div
                                class="add-to-cart flex gap-2 items-center justify-center rounded-md bg-[#007a3d] cursor-pointer px-5 py-2.5 text-center text-sm font-medium text-white transition-all hover:bg-[#117a3d] focus:outline-none">
                                <i class="fa-solid fa-cart-shopping"></i>
                                Add to cart
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </section>
    <!-- end in product bugs  -->

    <!-- start in  product travel -->
    <section class="py-5 mt-5">
        <div class="container mx-auto px-8">
            <div class="flex justify-between items-center">
                <h1 class="titleMain">luggage</h1>
                <a href="showProduct.html" class="text-[#3c3b6e] text-xl">view all</a>
            </div>
            <div class="product-card-container-outer">
                <div
                    class="product-card-container pt-4 grid lg:grid-cols-4 md:grid-cols-3 sm:grid-cols-2 grid-cols-1 justify-start items-center gap-4 transition-all delay-150 ease-in">
                    <!-- 1 -->
                    <div
                        class="productElement relative  flex w-full  flex-col overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg transition-all duration-300 hover:shadow-2xl hover:-translate-y-1 ">
                        <a class="relative mx-3 mt-3 flex h-60 overflow-hidden rounded-xl" href="detailsProduct.html">
                            <img class="imgProduct object-fill w-full"
                                src="puplic/images/WhatsApp Image 2023-08-10 at 11.43.22 PM.jpeg" alt="product image" />
                            <span
                                class="absolute top-0 left-0 m-2 rounded-full bg-[#b22234] px-2 text-center text-sm font-medium text-white">39%
                  OFF</span>
                        </a>
                        <div class="mt-4 px-5 pb-5">
                            <a href="detailsProduct.html">
                                <div>
                                    <h5 class="text-xl tracking-tight text-[#504f85] product-name">Nike Air MX Super 2500 - Red</h5>
                                </div>
                                <div class="mt-2 mb-5 flex items-center justify-between">
                                    <p>
                                        <span class="text-xl font-normal text-[#504f85] product-price uppercase">kd <span>449</span>
                                        </span>
                                        <span class="salePrice text-sm text-[#504f85] line-through uppercase">kd699</span>
                                    </p>

                                </div>
                            </a>
                            <div
                                class="add-to-cart flex gap-2 items-center justify-center rounded-md bg-[#007a3d] cursor-pointer px-5 py-2.5 text-center text-sm font-medium text-white transition-all hover:bg-[#117a3d] focus:outline-none">
                                <i class="fa-solid fa-cart-shopping"></i>
                                Add to cart
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </section>
    <!-- end in product travel  -->
</asp:Content>

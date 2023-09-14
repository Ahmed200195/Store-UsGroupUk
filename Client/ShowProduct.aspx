<%@ Page Title="" Language="C#" MasterPageFile="~/Client/Site1.Master" AutoEventWireup="true" CodeBehind="ShowProduct.aspx.cs" Inherits="Store.Client.ShowProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--  -->
    <section class="flex showProduct relative -top-7 sm:-top-11 lg:mt-32 mt-28">
        <div class="overlayShowProduct w-screen h-screen fixed z-50 top-0"></div>
        <!-- start open filter -->
        <div
            class="opneFilter hidden fixed top-1/4 bg-[#b22234] text-white  py-2 px-2 z-40 rounded-r cursor-pointer">
            <span class="capitalize tracking-wide"><%= Application["lang"].ToString() == "en" ? "Advanced Search" : "بحث متقدم" %></span>
        </div>
        <!-- end open filter -->
        <div class="filterElement relative w-1/4 h-screen overflow-scroll ">
            <!-- close filter -->
            <div class="closeFilter hidden px-4 border-b-2 py-2 cursor-pointer">
                <i class="fa-solid fa-xmark"></i>
            </div>
            <!-- close  filter -->
            <div class="listFilter relative md:absolute  z-10 border-r-4 transition-all duration-300"
                id="fixed-element">
                <!-- categories -->
                <!-- categories -->
                <div class="categories gap-4 border-b-2  w-full px-4">
                    <h1 class="mt-5 mb-2 capitalize font-bold text-xl text-[#3c3b6e]"><%= Application["lang"].ToString() == "en" ? "Categories" : "الاقسام" %></h1>
                    <ul>
                        <asp:Literal ID="ltCategories" runat="server"></asp:Literal>
                    </ul>
                </div>
                <!-- color -->
                <section class="px-4 pb-5 border-b-2">
                    <h1 class="mt-5 mb-2 capitalize font-bold text-xl text-[#3c3b6e]"><%= Application["lang"].ToString() == "en" ? "Color" : "اللون" %></h1>
                    <asp:DropDownList ID="ddlColors" AutoPostBack="true" CssClass="bg-gray-50 border border-gray-300  text-sm rounded-lg text-[#3c3v6e]  block w-full p-2.5  dark:border-gray-600 dark:placeholder-[#3c3b6e] capitalize" runat="server" DataSourceID="sqlColors" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="ddlColors_SelectedIndexChanged"></asp:DropDownList>
                    <asp:SqlDataSource ID="sqlColors" runat="server" ConnectionString='<%$ ConnectionStrings:dbUsGroupKw %>' SelectCommand="SELECT * FROM [Color] ORDER BY [Id]"></asp:SqlDataSource>
                </section>
                <!-- size -->
                <div class="size gap-4 border-b-2 px-4 pb-5">
                    <h1 class="mt-5 mb-2  capitalize font-bold text-xl text-[#3c3b6e]"><%= Application["lang"].ToString() == "en" ? "Size" : "القياس" %></h1>
                    <div class="checkElement flex flex-wrap gap-2">
                        <asp:Literal ID="ltSizes" runat="server"></asp:Literal>
                    </div>
                </div>
                <!-- sort -->
                <section class="px-4 pb-5 border-b-2">
                    <h1 class="mt-5 mb-2 capitalize font-bold text-xl text-[#3c3b6e]"><%= Application["lang"].ToString() == "en" ? "Sort By" : "ترتيب حسب" %></h1>
                    <asp:DropDownList ID="ddlSortBy" CssClass="bg-gray-50 border border-gray-300  text-sm rounded-lg text-[#3c3v6e]  block w-full p-2.5  dark:border-gray-600 dark:placeholder-[#3c3b6e] capitalize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Text="default" />
                        <asp:ListItem Text="name" />
                        <asp:ListItem Text="price:low to high" />
                        <asp:ListItem Text="price:high to low" />
                    </asp:DropDownList>
                </section>
            </div>
        </div>

        <div class="showElement relative w-3/4 h-screen mt-6 lg:mt-0 lg:px-2 overflow-scroll ">
            <div class="container mx-auto px-2 py-5">
                <div class="flex items-center justify-between text-sm tracking-widest uppercase">
                    <p id="cntProduct" class="text-[#3c3b6e] font-bold" runat="server">6 Items</p>

                </div>
                <div class="grid gap-2 grid-cols-1 lg:gap-4 md:grid-cols-3 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 mt-8">
                    <!-- 1 -->
                    <asp:Literal ID="ltProdcut" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

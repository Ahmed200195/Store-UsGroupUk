﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Client
{
    public partial class Home : Page
    {
        DataTable dtDept, dtProduct;
        ClsBasic clsBasic;
        protected void Page_Load(object sender, EventArgs e)
        {
            clsBasic = new ClsBasic();
            dtDept = clsBasic.SelectData("*", "Dept WHERE Id IN (SELECT DId FROM Product)");
            dtProduct = clsBasic.SelectData("*", "Product");
            ltImgSlider.Text = "";
            foreach (DataRow row in clsBasic.SelectData("*", "ProductPhotos WHERE PId IS NULL").Rows)
            {
                ltImgSlider.Text += $@"
                            <div class=""slide w-full h-full rounded-xl absolute transition-all duration-500"" data-slide>
                                <img src=""data:image;base64,{Convert.ToBase64String((byte[])row["Photo"])}"" class=""w-full h-full  object-cover md:object-fill"">
                            </div>";
            }
            ltCategory.Text = string.Empty;
            foreach (DataRow row in dtDept.Rows)
            {
                ltCategory.Text += $@"
                        <div class=""carousel-cell"">
                          <div class=""flex flex-col justify-center items-center gap-4 md:mr-4 mr-2"">
                            <a href=""ShowProduct.aspx?dept={row["Id"]}"" target=""_blank""
                            <figure class=""relative rounded-lg"">
                              <img src=""data:image;base64,{Convert.ToBase64String((byte[])row["Photo"])}"" class=""rounded-lg object-fill"" alt="""" />
                            </figure>
                            </a>
                            <h1 class=""cursor-pointer text-center capitalize text-[#b22234] font-semibold text-xl"">
                              {row["NameEn"]}
                            </h1>
                          </div>
                        </div>
                ";
            }
            ltFeaturedProduct.Text = "";
            string disc = string.Empty, finalPrice = string.Empty, discPrice = string.Empty;
            foreach (DataRow row in clsBasic.SelectData("TOP 4 *", "Product").Rows)
            {
                finalPrice = row["Price"].ToString();
                if (int.Parse(row["Discount"].ToString()) > 0)
                {
                    disc = $@"<span
                            class=""absolute top-0 left-0 m-2 rounded-full bg-[#b22234] px-2 text-center text-sm font-medium text-white"">{100 - ((int.Parse(row["Discount"].ToString()) * 100) / int.Parse(row["Price"].ToString()))}%
                    OFF</span>";

                    discPrice = $@"<span class=""salePrice text-sm text-[#504f85] line-through uppercase"">${row["Price"]}</span>";
                    finalPrice = row["Discount"].ToString();
                }
                ltFeaturedProduct.Text += $@"
                    <div
                    class=""productElement relative  flex w-full  flex-col overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg transition-all duration-300 hover:shadow-2xl hover:-translate-y-1 "">
                    <a class=""relative mx-3 mt-3 flex h-60 overflow-hidden rounded-xl"" href=""DetailsProduct.aspx?id={row["Id"]}"" target=""_blank"">
                        <img class=""imgProduct object-fill w-full h-full"" src=""data:image;base64,{Convert.ToBase64String((byte[])row["Photo"])}""
                            alt=""product image"" />
                        {disc}
                    </a>
                    <div class=""mt-4 px-5 pb-5"">
                        <a href=""DetailsProduct.aspx?id={row["Id"]}"" target=""_blank"">
                            <div>
                                <h5 class=""text-xl tracking-tight text-[#504f85] product-name"">{row["NameEn"]}</h5>
                            </div>
                            <div class=""mt-2 mb-5 flex items-center justify-between"">
                                <p>
                                    <span class=""text-xl font-normal text-[#504f85] product-price uppercase"">$<span>{finalPrice}</span>
                                    </span>
                                    {discPrice}
                                </p>

                            </div>
                        </a>
                        <div
                            class=""add-to-cart flex gap-2 items-center justify-center rounded-md bg-[#007a3d] cursor-pointer px-5 py-2.5 text-center text-sm font-medium text-white transition-all hover:bg-[#117a3d] focus:outline-none"">
                            <i class=""fa-solid fa-cart-shopping""></i>
                            Add to cart
                        </div>
                    </div>
                </div>
                ";
                disc = string.Empty; discPrice = string.Empty;
            }

            ltProdcutsByDept.Text = "";
            foreach (DataRow dept in dtDept.Rows)
            {
                ltProdcutsByDept.Text += $@"
                                     <section class=""py-5 mt-5"">
                            <div class=""container mx-auto px-8"">
                                <div class=""flex justify-between items-center"">
                                    <h1 class=""titleMain"">{dept["NameEn"]}</h1>
                                    <a href=""ShowProduct.aspx?dept={dept["Id"]}"" target=""_blank"" class=""text-[#3c3b6e] text-xl"">view all</a>
                                </div>
                                <div class=""product-card-container-outer"">
                                <div class=""product-card-container pt-4 grid lg:grid-cols-4 md:grid-cols-3 sm:grid-cols-2 grid-cols-1 justify-start items-center gap-4 transition-all delay-150 ease-in"">";
                foreach (DataRow product in clsBasic.SelectData("TOP 4 *", "Product WHERE DId = " + dept["Id"]).Rows)
                {
                    finalPrice = product["Price"].ToString();
                    if (int.Parse(product["Discount"].ToString()) > 0)
                    {
                        disc = $@"<span
                            class=""absolute top-0 left-0 m-2 rounded-full bg-[#b22234] px-2 text-center text-sm font-medium text-white"">{100 - ((int.Parse(product["Discount"].ToString()) * 100) / int.Parse(product["Price"].ToString()))}% OFF</span>";

                        discPrice = $@"<span class=""salePrice text-sm text-[#504f85] line-through uppercase"">${product["Price"]}</span>";
                        finalPrice = product["Discount"].ToString();
                    }
                    ltProdcutsByDept.Text += $@"
                                    
                                        <!-- 1 -->
                                        <div
                                            class=""productElement relative  flex w-full  flex-col overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg transition-all duration-300 hover:shadow-2xl hover:-translate-y-1 "">
                                            <a class=""relative mx-3 mt-3 flex h-60 overflow-hidden rounded-xl"" href=""DetailsProduct.aspx?id={product["Id"]}"" target=""_blank"">
                                                <img class=""imgProduct object-fill w-full h-full"" src=""data:image;base64,{Convert.ToBase64String((byte[])product["Photo"])}""
                                                    alt=""product image"" />
                                            {disc}
                                            </a>
                                            <div class=""mt-4 px-5 pb-5"">
                                                <a href=""DetailsProduct.aspx?id={product["Id"]}"" target=""_blank"">
                                                    <div>
                                                        <h5 class=""text-xl tracking-tight text-[#504f85] product-name"">{product["NameEn"]}</h5>
                                                    </div>
                                                    <div class=""mt-2 mb-5 flex items-center justify-between"">
                                                        <p>
                                                            <span class=""text-xl font-normal text-[#504f85] product-price uppercase"">$<span>{finalPrice}</span>
                                                            </span>
                                                            {discPrice}
                                                        </p>

                                                    </div>
                                                </a>
                                                <!-- add to cart -->
                                                <div
                                                    class=""add-to-cart flex gap-2 items-center justify-center rounded-md bg-[#007a3d] cursor-pointer px-5 py-2.5 text-center text-sm font-medium text-white transition-all hover:bg-[#117a3d] focus:outline-none"">
                                                    <i class=""fa-solid fa-cart-shopping""></i>
                                                    Add to cart
                                                </div>
                                            </div>
                                        </div>


                                    
                    ";
                    disc = string.Empty; discPrice = string.Empty;
                }
                ltProdcutsByDept.Text += @"
                                    </div>
                                </div>
                            </div>
                        </section>";
            }
            
        }
    }
}
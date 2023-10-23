using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace Store.Client
{
    public partial class Home : Page
    {
        DataSet dataSet;
        DataTable dtDept;
        ClsBasic clsBasic;
        DataRow[] rowProduct;
        string viewAll, Name, Size, Color, addToCart, Off;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lang"] == null)
            {
                Session["lang"] = "ar";
            }
            Page.Title = Session["lang"].ToString() == "en" ? "UsGroupUk | Home" : "UsGroupUk | صفحة الرئيسية";
            //language
                viewAll = Session["lang"].ToString() == "en" ? "View All" : "عرض الكل";
                Name = Session["lang"].ToString() == "en" ? "NameEn" : "NameAr";
                Size = Session["lang"].ToString() == "en" ? "Size" : "القياس";
                Color = Session["lang"].ToString() == "en" ? "Color" : "اللون";
                addToCart = Session["lang"].ToString() == "en" ? "Add to cart" : "أضف إلى السلة";
                Off = Session["lang"].ToString() == "en" ? "OFF" : "خصم";
            
            clsBasic = new ClsBasic();
            string queries = @"
                SELECT * FROM Dept WHERE Id IN (SELECT DId FROM Product WHERE Cnt > 0);
                SELECT * FROM ProductPhotos WHERE PId IS NULL;
                WITH TopProducts AS (SELECT TOP 4 COUNT(*) as CntProduct, PId FROM Orders GROUP BY PId ORDER BY 1 DESC)
                SELECT Product.Id, NameAr, NameEn, Price, Discount, Color.Name as ColorName, Size.Name as SizeNum, Photo, Cnt 
                FROM Product FULL OUTER JOIN [Color] ON Color.Id = Product.CId FULL OUTER JOIN [Size] ON Size.Id = Product.SId WHERE Cnt > 0 AND Product.Id IN (SELECT TOP 4 Id FROM Product WHERE Id IN (SELECT PId FROM TopProducts)) ORDER BY NEWID();
                SELECT Product.Id, NameAr, NameEn, Price, Discount, Color.Name as ColorName, Size.Name as SizeNum, Photo, Cnt, DId 
                FROM Product FULL OUTER JOIN [Color] ON Color.Id = Product.CId FULL OUTER JOIN [Size] ON Size.Id = Product.SId WHERE Cnt > 0 ORDER BY NEWID();
            ";
            dataSet = clsBasic.SelectMultiple(queries);
            dtDept = dataSet.Tables[0];
            ltImgSlider.Text = "";
            foreach (DataRow row in dataSet.Tables[1].Rows)
            {
                ltImgSlider.Text += $@"
                            <div class=""slide w-full h-full rounded-xl absolute transition-all duration-500"" data-slide>
                                <img data-src=""../Uploads/Product/{row["Name"]}"" class=""w-full h-full lazy-load object-cover md:object-fill"" >
                            </div>";
            }
            ltCategory.Text = string.Empty;
            foreach (DataRow row in dtDept.Rows)
            {
                ltCategory.Text += $@"
                        <div class=""carousel-cell"">
                          <div class=""flex flex-col justify-center items-center gap-4 md:mr-4 mr-2"">
                            <figure class=""relative rounded-lg"">
                              <img data-src=""../Uploads/Dept/{row["Photo"]}"" class=""lazy-load rounded-lg object-fill""  />
                            </figure>
                            <a href=""ShowProduct.aspx?dept={row["Id"]}&size=0&color=0&sortBy=0&search=0"" target=""_self""
                            <h1 class=""cursor-pointer text-center capitalize text-[#b22234] font-semibold text-xl"">
                              {row[Name]}
                            </h1>
                            </a>
                          </div>
                        </div>
                ";
            }
            ltFeaturedProduct.Text = "";
            string disc = string.Empty, finalPrice = string.Empty, discPrice = string.Empty;

            if (dataSet.Tables[2].Rows.Count == 0)
            {
                dvFeatured.Visible = false;
            }
            else
            {
                dvFeatured.Visible = true;
                foreach (DataRow row in dataSet.Tables[2].Rows)
                {
                    finalPrice = row["Price"].ToString();
                    if (int.Parse(row["Discount"].ToString()) > 0)
                    {
                        disc = $@"<span
                            class=""absolute top-0 left-0 m-2 rounded-full bg-[#b22234] px-2 text-center text-sm font-medium text-white"">{100 - ((int.Parse(row["Discount"].ToString()) * 100) / int.Parse(row["Price"].ToString()))}%
                    {Off}</span>";

                        discPrice = $@"<span class=""salePrice text-sm text-[#504f85] line-through uppercase"">${row["Price"]}</span>";
                        finalPrice = row["Discount"].ToString();
                    }
                    ltFeaturedProduct.Text += $@"
                    <div
                    class=""productElement relative  flex w-full  flex-col overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg transition-all duration-300 hover:shadow-2xl hover:-translate-y-1 h-full"" data-id=""{row["Id"]}"" data-count=""{row["Cnt"]}"">
                    <a class=""relative mx-3 mt-3 flex h-60 overflow-hidden rounded-xl"" href=""DetailsProduct.aspx?id={row["Id"]}&cnt={row["Cnt"]}"" target=""_self"">
                        <img class=""lazy-load imgProduct object-fill w-full h-full"" data-src=""../Uploads/Product/{row["Photo"]}""
                            alt=""product image""  />
                        {disc}
                    </a>
                    <div class=""mt-4 px-5 pb-5"">
                        <a href=""DetailsProduct.aspx?id={row["Id"]}&cnt={row["Cnt"]}"" target=""_self"">
                            <div>
                                <h5 class=""text-xl tracking-tight text-[#504f85] product-name"">{row[Name]}</h5>
                            </div>
                            <div class=""mt-2 mb-5 flex items-center justify-between"">
                                <p>
                                    <span class=""text-xl font-normal text-[#504f85] product-price uppercase"">$<span>{finalPrice}</span>
                                    </span>
                                    {discPrice}
                                </p>

                            </div>
                        </a>
                         <!-- size  color -->
                        <div class=""flex justify-between mb-2"">
                    ";
                    if (row["ColorName"] != DBNull.Value)
                    {
                        ltFeaturedProduct.Text += $@"
                            <h1 class=""capitalize color"">{Color}: <span class=""capitalize"">{row["ColorName"]}</span></h1>
                        ";
                    }
                    if (row["SizeNum"] != DBNull.Value)
                    {
                        ltFeaturedProduct.Text += $@"
                            <h1 class=""capitalize size"">{Size}: <span class=""capitalize"">{row["SizeNum"]}</span></h1>
                        ";
                    }
                    ltFeaturedProduct.Text += $@"
                        </div>
                        <div
                            class=""add-to-cart flex gap-2 items-center justify-center rounded-md bg-[#007a3d] cursor-pointer px-5 py-2.5 text-center text-sm font-medium text-white transition-all hover:bg-[#117a3d] focus:outline-none"">
                            <i class=""fa-solid fa-cart-shopping""></i>
                            {addToCart}
                        </div>
                    </div>
                </div>
                ";
                    disc = string.Empty; discPrice = string.Empty;
                }
            }

            ltProdcutsByDept.Text = "";
            int cnt = 1;
            
            foreach (DataRow dept in dtDept.Rows)
            {
                ltProdcutsByDept.Text += $@"
                                     <section class=""py-5 mt-5"">
                            <div class=""container mx-auto px-8"">
                                <div class=""flex justify-between items-center"">
                                    <h1 class=""titleMain"">{dept[Name]}</h1>
                                    <a href=""ShowProduct.aspx?dept={dept["Id"]}&size=0&color=0&sortBy=0&search=0"" target=""_self"" class=""text-[#3c3b6e] text-xl"">{viewAll}</a>
                                </div>
                                <div class=""product-card-container-outer"">
                                <div class=""product-card-container pt-4 grid lg:grid-cols-4 md:grid-cols-3 sm:grid-cols-2 grid-cols-1 justify-start items-center gap-4 transition-all delay-150 ease-in"">";
                rowProduct = dataSet.Tables[3].Select("DId = " + int.Parse(dept["Id"].ToString()));
                foreach (DataRow product in rowProduct)
                {
                    if (cnt > 4)
                    {
                        break;
                    }
                    finalPrice = product["Price"].ToString();
                    if (int.Parse(product["Discount"].ToString()) > 0)
                    {
                        disc = $@"<span
                            class=""absolute top-0 left-0 m-2 rounded-full bg-[#b22234] px-2 text-center text-sm font-medium text-white"">{100 - ((int.Parse(product["Discount"].ToString()) * 100) / int.Parse(product["Price"].ToString()))}% {Off}</span>";

                        discPrice = $@"<span class=""salePrice text-sm text-[#504f85] line-through uppercase"">${product["Price"]}</span>";
                        finalPrice = product["Discount"].ToString();
                    }
                    ltProdcutsByDept.Text += $@"
                                        <div
                                            class=""productElement relative  flex w-full  flex-col overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg transition-all duration-300 hover:shadow-2xl hover:-translate-y-1 h-full"" data-id=""{product["Id"]}""  data-count=""{product["Cnt"]}"">
                                            <a class=""relative mx-3 mt-3 flex h-60 overflow-hidden rounded-xl"" href=""DetailsProduct.aspx?id={product["Id"]}&cnt={product["Cnt"]}"" target=""_self"">
                                                <img class=""imgProduct object-fill w-full h-full lazy-load"" data-src=""../Uploads/Product/{product["Photo"]}""
                                                    alt=""product image""  />
                                            {disc}
                                            </a>
                                            <div class=""mt-4 px-5 pb-5"">
                                                <a href=""DetailsProduct.aspx?id={product["Id"]}&cnt={product["Cnt"]}"" target=""_self"">
                                                    <div>
                                                        <h5 class=""text-xl tracking-tight text-[#504f85] product-name"">{product[Name]}</h5>
                                                    </div>
                                                    <div class=""mt-2 mb-5 flex items-center justify-between"">
                                                        <p>
                                                            <span class=""text-xl font-normal text-[#504f85] product-price uppercase"">$<span>{finalPrice}</span>
                                                            </span>
                                                            {discPrice}
                                                        </p>

                                                    </div>
                                                </a>
                                               
                        <div class=""flex justify-between mb-2"">
                    ";
                    if (product["ColorName"] != DBNull.Value)
                    {
                        ltProdcutsByDept.Text += $@"
                            <h1 class=""capitalize color"">{Color}: <span class=""capitalize"">{product["ColorName"]}</span></h1>
                        ";
                    }
                    if (product["SizeNum"] != DBNull.Value)
                    {
                        ltProdcutsByDept.Text += $@"
                            <h1 class=""capitalize size"">{Size}: <span class=""capitalize"">{product["SizeNum"]}</span></h1>
                        ";
                    }

                    ltProdcutsByDept.Text += $@"
                        </div>
                                                <!-- add to cart -->
                                                <div
                                                    class=""add-to-cart flex gap-2 items-center justify-center rounded-md bg-[#007a3d] cursor-pointer px-5 py-2.5 text-center text-sm font-medium text-white transition-all hover:bg-[#117a3d] focus:outline-none"">
                                                    <i class=""fa-solid fa-cart-shopping""></i>
                                                    {addToCart}
                                                </div>
                                            </div>
                                        </div>
                    ";
                    disc = string.Empty; discPrice = string.Empty;
                    cnt++;
                }
                cnt = 1;
                ltProdcutsByDept.Text += @"
                                    </div>
                                </div>
                            </div>
                        </section>";
            }
        }
    }
}
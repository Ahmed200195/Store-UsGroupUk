using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace Store.Client
{
    public partial class Home : Page
    {
        DataTable dtDept;
        ClsBasic clsBasic;
        string viewAll, Name, Size, Color, addToCart, Off;
        protected void Page_Load(object sender, EventArgs e)
        {
            //language
                viewAll = Application["lang"].ToString() == "en" ? "View All" : "عرض الكل";
                Name = Application["lang"].ToString() == "en" ? "NameEn" : "NameAr";
                Size = Application["lang"].ToString() == "en" ? "Size" : "القياس";
                Color = Application["lang"].ToString() == "en" ? "Color" : "اللون";
                addToCart = Application["lang"].ToString() == "en" ? "Add to cart" : "أضف إلى السلة";
                Off = Application["lang"].ToString() == "en" ? "OFF" : "خصم";
            



            clsBasic = new ClsBasic();
            dtDept = clsBasic.SelectData("*", "Dept WHERE Id IN (SELECT DId FROM Product)");
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
                            <figure class=""relative rounded-lg"">
                              <img src=""data:image;base64,{Convert.ToBase64String((byte[])row["Photo"])}"" class=""rounded-lg object-fill"" alt="""" />
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

            DataTable dataTable = clsBasic.SelectData("TOP 4 Product.Id, NameAr, NameEn, Price, Discount, Color.Name as ColorName, Size.Name as SizeNum, Photo, Cnt", " Product INNER JOIN [Color] ON Color.Id = Product.CId INNER JOIN [Size] ON Size.Id = Product.SId AND Cnt > 0 AND Product.Id IN (SELECT PId FROM Orders GROUP BY PId HAVING COUNT(*) >= 3) ORDER BY NEWID()");
            if (dataTable.Rows.Count < 4)
            {
                dataTable = clsBasic.SelectData("TOP 4 Product.Id, NameAr, NameEn, Price, Discount, Color.Name as ColorName, Size.Name as SizeNum, Photo, Cnt", " Product INNER JOIN [Color] ON Color.Id = Product.CId INNER JOIN [Size] ON Size.Id = Product.SId AND Cnt > 0 ORDER BY NEWID()");
            }
            foreach (DataRow row in dataTable.Rows)
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
                    class=""productElement relative  flex w-full  flex-col overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg transition-all duration-300 hover:shadow-2xl hover:-translate-y-1 "" data-id=""{row["Id"]}"" data-count=""{row["Cnt"]}"">
                    <a class=""relative mx-3 mt-3 flex h-60 overflow-hidden rounded-xl"" href=""DetailsProduct.aspx?id={row["Id"]}"" target=""_blank"">
                        <img class=""imgProduct object-fill w-full h-full"" src=""data:image;base64,{Convert.ToBase64String((byte[])row["Photo"])}""
                            alt=""product image"" />
                        {disc}
                    </a>
                    <div class=""mt-4 px-5 pb-5"">
                        <a href=""DetailsProduct.aspx?id={row["Id"]}"" target=""_blank"">
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
                            <h1 class=""capitalize color"">{Color}: <span class=""capitalize"">{row["ColorName"]}</span></h1>
                            <h1 class=""capitalize size"">{Size}: <span class=""capitalize"">{row["SizeNum"]}</span></h1>
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

            ltProdcutsByDept.Text = "";
            foreach (DataRow dept in dtDept.Rows)
            {
                ltProdcutsByDept.Text += $@"
                                     <section class=""py-5 mt-5"">
                            <div class=""container mx-auto px-8"">
                                <div class=""flex justify-between items-center"">
                                    <h1 class=""titleMain"">{dept[Name]}</h1>
                                    <a href=""ShowProduct.aspx?dept={dept["Id"]}&size=0&color=0&sortBy=0&search=0"" target=""_blank"" class=""text-[#3c3b6e] text-xl"">{viewAll}</a>
                                </div>
                                <div class=""product-card-container-outer"">
                                <div class=""product-card-container pt-4 grid lg:grid-cols-4 md:grid-cols-3 sm:grid-cols-2 grid-cols-1 justify-start items-center gap-4 transition-all delay-150 ease-in"">";
                foreach (DataRow product in clsBasic.SelectData("TOP 4 Product.Id, NameAr, NameEn, Price, Discount, Color.Name as ColorName, Size.Name as SizeNum, Photo, Cnt", "Product INNER JOIN [Color] ON Color.Id = Product.CId INNER JOIN [Size] ON Size.Id = Product.SId WHERE DId = " + dept["Id"] + " AND Cnt > 0 ORDER BY NEWID()").Rows)
                {
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
                                            class=""productElement relative  flex w-full  flex-col overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg transition-all duration-300 hover:shadow-2xl hover:-translate-y-1 "" data-id=""{product["Id"]}""  data-count=""{product["Cnt"]}"">
                                            <a class=""relative mx-3 mt-3 flex h-60 overflow-hidden rounded-xl"" href=""DetailsProduct.aspx?id={product["Id"]}"" target=""_blank"">
                                                <img class=""imgProduct object-fill w-full h-full"" src=""data:image;base64,{Convert.ToBase64String((byte[])product["Photo"])}""
                                                    alt=""product image"" />
                                            {disc}
                                            </a>
                                            <div class=""mt-4 px-5 pb-5"">
                                                <a href=""DetailsProduct.aspx?id={product["Id"]}"" target=""_blank"">
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
                                                <!-- size  color -->
                                                <div class=""flex justify-between mb-2"">
                                                    <h1 class=""capitalize color"">{Color}: <span class=""capitalize"">{product["ColorName"]}</span></h1>
                                                    <h1 class=""capitalize size"">{Size}: <span class=""capitalize"">{product["SizeNum"]}</span></h1>
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
                }
                ltProdcutsByDept.Text += @"
                                    </div>
                                </div>
                            </div>
                        </section>";
            }
        }

        [WebMethod]
        public static void PaidCheckOut(string dvInfoProduct, List<string> list)
        {
            //ClsBasic clsBasic = new ClsBasic();
            //clsBasic.newOrder(dvInfoProduct, list);
        }

    }
}
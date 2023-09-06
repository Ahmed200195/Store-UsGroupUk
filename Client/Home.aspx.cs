using System;
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
        ClsBasic clsBasic;
        protected void Page_Load(object sender, EventArgs e)
        {
            clsBasic = new ClsBasic();
            ltImgSlider.Text = "";
            foreach (DataRow row in clsBasic.SelectData("*", "ProductPhotos WHERE PId IS NULL").Rows)
            {
                ltImgSlider.Text += $@"
                            <div class=""slide w-full h-full rounded-xl absolute transition-all duration-500"" data-slide>
                                <img src=""data:image;base64,{Convert.ToBase64String((byte[])row["Photo"])}"" class=""w-full h-full  object-cover md:object-fill"">
                            </div>";
            }
            ltCategory.Text = string.Empty;
            foreach (DataRow row in clsBasic.SelectData("*", "Dept").Rows)
            {
                ltCategory.Text += $@"
                        <div class=""carousel-cell"">
                            <div class=""flex flex-col justify-center items-center gap-4 md:mr-4 mr-2"">
                                <figure class=""relative rounded-lg"">
                                    <img src=""data:image;base64,{Convert.ToBase64String((byte[])row["Photo"])}"" class=""rounded-lg object-fill"" alt="""" />
                                </figure>
                                <h1 class=""cursor-pointer text-center capitalize text-[#b22234] font-semibold text-xl"">{row["NameEn"]}
                                </h1>
                            </div>
                        </div>
                ";
            }

            foreach (DataRow row in clsBasic.SelectData("*", "Product").Rows)
            {

            }
        }
    }
}
using System.Drawing.Text;

namespace Functional_Messeenger_Client
{
    class FontBox
    {
        static public PrivateFontCollection externalFont = new PrivateFontCollection();
        static public void init()
        {
            externalFont.AddFontFile("NanumSquareB.ttf");
            externalFont.AddFontFile("NanumSquareEB.ttf");
            externalFont.AddFontFile("NanumSquareL.ttf");
            externalFont.AddFontFile("NanumSquareR.ttf");
        }
    }
}

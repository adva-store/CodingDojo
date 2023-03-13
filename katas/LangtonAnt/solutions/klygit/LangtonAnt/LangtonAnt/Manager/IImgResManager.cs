using System;
using Xamarin.Forms;

namespace LangtonAnt
{
    internal interface IImgResManager
    {
        ImageSource GetImgSrc(string imgSrcFilename);

        ImageSource GetImgSrc(Type type, string imgSrcFoldername, string imgSrcFilename);

        ImageSource GetImgSrc(Type type, string imgSrcPath);
    }
}

using System;
using System.Reflection;
using Xamarin.Forms;

namespace LangtonAnt
{
    public class ImgResManager : IImgResManager
    {
        private static readonly string IMG_SRC__FOLDERNAME = "Res.Img";
        public ImageSource GetImgSrc(string imgSrcFilename) => GetImgSrc(typeof(ImgResHelper), IMG_SRC__FOLDERNAME, imgSrcFilename);

        public ImageSource GetImgSrc(Type type, string imgSrcFoldername, string imgSrcFilename)
        {
            return GetImgSrc(type, GetImgSrcPath(type.Namespace, imgSrcFoldername, imgSrcFilename));
        }

        public ImageSource GetImgSrc(Type type, string imgSrcPath)
        {
            return ImageSource.FromResource(imgSrcPath, type.GetTypeInfo()?.Assembly);
        }

        private static string GetImgSrcPath(string imgSrcNamespace, string imgSrcFolderName, string imgSrcFilename)
        {
            return string.Format("{0}.{1}.{2}", imgSrcNamespace, imgSrcFolderName, imgSrcFilename);
        }
    }
}

using System;
using am.kon.packages.services.WebClientService.Constants;

namespace am.kon.packages.services.WebClientService.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="HttpContentMediaTypes"/>.
    /// </summary>
    public static class HttpContentMediaTypesExtensions
    {
        /// <summary>
        /// Extension method to get string representation for instance of <see cref="HttpContentMediaTypes"/>.
        /// </summary>
        /// <param name="mediaType">Instance of <see cref="HttpContentMediaTypes"/> to get string representation for.</param>
        /// <returns>String representation of the instance of <see cref="HttpContentMediaTypes"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Trows when invalid value was provided for <see cref="HttpContentMediaTypes"/>.</exception>
        public static string GetMimeType(this HttpContentMediaTypes mediaType)
        {
            return mediaType switch
            {
                HttpContentMediaTypes.TextPlain => HttpContentMediaTypesConstants.TextPlain,
                HttpContentMediaTypes.TextHtml => HttpContentMediaTypesConstants.TextHtml,
                HttpContentMediaTypes.TextCss => HttpContentMediaTypesConstants.TextCss,
                HttpContentMediaTypes.ApplicationJson => HttpContentMediaTypesConstants.ApplicationJson,
                HttpContentMediaTypes.ApplicationXml => HttpContentMediaTypesConstants.ApplicationXml,
                HttpContentMediaTypes.ApplicationFormUrlEncoded => HttpContentMediaTypesConstants.ApplicationFormUrlEncoded,
                HttpContentMediaTypes.ApplicationOctetStream => HttpContentMediaTypesConstants.ApplicationOctetStream,
                HttpContentMediaTypes.MultipartFormData => HttpContentMediaTypesConstants.MultipartFormData,
                HttpContentMediaTypes.ImagePng => HttpContentMediaTypesConstants.ImagePng,
                HttpContentMediaTypes.ImageJpeg => HttpContentMediaTypesConstants.ImageJpeg,
                HttpContentMediaTypes.ImageGif => HttpContentMediaTypesConstants.ImageGif,
                HttpContentMediaTypes.AudioMpeg => HttpContentMediaTypesConstants.AudioMpeg,
                HttpContentMediaTypes.AudioOgg => HttpContentMediaTypesConstants.AudioOgg,
                HttpContentMediaTypes.VideoMp4 => HttpContentMediaTypesConstants.VideoMp4,
                HttpContentMediaTypes.VideoOgg => HttpContentMediaTypesConstants.VideoOgg,
                _ => throw new ArgumentOutOfRangeException(nameof(mediaType), mediaType, null)
            };
        }
    }
}
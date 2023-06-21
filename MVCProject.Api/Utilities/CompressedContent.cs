// -----------------------------------------------------------------------
// <copyright file="CompressedContent.cs" company="ASK E-Sqaure">
// All copy rights reserved @ASK E-Sqaure.
// </copyright>
// ----------------------------------------------------------------------- 

namespace MVCProject.Api.Utilities
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Compressed result content for GZip Compression 
    /// </summary>
    public class CompressedContent : HttpContent
    {
        /// <summary>
        /// encoding type
        /// </summary>
        private readonly string encodingType;

        /// <summary>
        /// original content
        /// </summary>
        private readonly HttpContent originalContent;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressedContent" /> class.        
        /// </summary>
        /// <param name="content">HttpContent contents</param>
        /// <param name="encodingType">encoding Type</param>
        public CompressedContent(HttpContent content, string encodingType = "gzip")
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            this.originalContent = content;
            this.encodingType = encodingType.ToLowerInvariant();

            foreach (var header in this.originalContent.Headers)
            {
                Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            Headers.ContentEncoding.Add(this.encodingType);
        }

        /// <summary>
        /// Compute length
        /// </summary>
        /// <param name="length">return length in out parameter</param>
        /// <returns>returns true/false</returns>
        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }

        /// <summary>
        /// Serialize to stream
        /// </summary>
        /// <param name="stream">stream to Serialize</param>
        /// <param name="context">context data</param>
        /// <returns>Returns response type of<see cref="Task"/>class </returns>
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            Stream compressedStream = null;
            switch (this.encodingType)
            {
                case "gzip":
                    compressedStream = new GZipStream(stream, CompressionMode.Compress, true);
                    break;
                case "deflate":
                    compressedStream = new DeflateStream(stream, CompressionMode.Compress, true);
                    break;
                default:
                    compressedStream = stream;
                    break;
            }

            return this.originalContent.CopyToAsync(compressedStream).ContinueWith(tsk =>
            {
                if (compressedStream != null)
                {
                    compressedStream.Dispose();
                }
            });
        }
    }
}
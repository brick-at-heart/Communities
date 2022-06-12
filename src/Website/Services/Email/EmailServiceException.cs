using System;

namespace BrickAtHeart.Communities.Services.Email
{
    public class EmailServiceException : ApplicationException
    {
        /// <summary>
        ///  Instantiates a new instance with a default message
        /// </summary>
        /// <param name="innerException">
        ///  An inner exception
        /// </param>
        public EmailServiceException( Exception innerException ) :
            base( "An error was encountered when sending the message.",
                  innerException )
        {
        }

        /// <summary>
        ///  Instantiates a new instance
        /// </summary>
        /// <param name="message">
        ///  A message which provides information about the exception
        /// </param>
        /// <param name="innerException">
        ///  An Inner exception
        /// </param>
        public EmailServiceException( string message,
                                      Exception innerException ) :
            base( message,
                  innerException )
        {
        }
    }
}
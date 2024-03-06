using System;
using Moq;
using Application.UnitTests.Documents.DataGenerator;
using ChequeMicroservice.Application.Common.Interfaces;
using OnyxDoc.AsposeService.Application.Common.Interfaces;
using System.Text;
using ChequeMicroservice.Application.Common.Models.Response;

namespace Application.UnitTests.Documents
{
    public class ServiceMockUp
    {
        public static class IBase64ToFileConverterMockUp
        {
            public static Mock<IBase64ToFileConverter> GetIBase64ToFileConverterService()
            {
                var mockIBase64ToFileConverter = new Mock<IBase64ToFileConverter>();
                mockIBase64ToFileConverter.Setup(x => x.ConvertBase64StringToFile(It.IsAny<string>(), It.IsAny<string>()));
                mockIBase64ToFileConverter.Setup(x => x.ToString()).Returns("");
                return mockIBase64ToFileConverter;
            }
        }

        public static class IBlobStorageServiceMockUp
        {
            public static Mock<IBlobStorageService> GetIBlobStorageService()
            {
                var mockIIBlobStorageService = new Mock<IBlobStorageService>();
                mockIIBlobStorageService.Setup(x => x.UploadFileToBlobAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<string>())).ReturnsAsync(FakeDataGenerator.FilePath);
                mockIIBlobStorageService.Setup(x => x.ToString()).Returns("https://flowmonostorage.blob.core.windows.net/uploads/tobi.osimosu@reventtechnologies.com_638405855340471400.pdf");
                return mockIIBlobStorageService;
            }
        }

        public static class IDocumentConverterServiceMockUp
        {
            public static Mock<IDocumentConverterService> GetIDocumentConverterService()
            {
                byte[] pdfArray = Encoding.UTF8.GetBytes("Sample PDF Data");
                var mockIDocumentConverterService = new Mock<IDocumentConverterService>();
                mockIDocumentConverterService.Setup(x => x.ConvertDocToPdfAspose(It.IsAny<byte[]>())).ReturnsAsync(pdfArray);
                mockIDocumentConverterService.Setup(x => x.ConvertImageToPDF(It.IsAny<byte[]>())).ReturnsAsync(pdfArray);
                mockIDocumentConverterService.Setup(x => x.ToString()).Returns("https://flowmonostorage.blob.core.windows.net/uploads/tobi.osimosu@reventtechnologies.com_638405855340471400.pdf");
                return mockIDocumentConverterService;
            }
        }

        public static class IDocumentManagementServiceMockUp
        {
            public static Mock<IDocumentManagementService> GetIDocumentManagementService()
            {
                //string filePath = "test.pdf";
                byte[] fileBytes = new byte[] { 1, 2, 3 };
                byte[] pdfArray = Encoding.UTF8.GetBytes("Sample PDF Data");
                var mockIDocumentManagementService = new Mock<IDocumentManagementService>();
                mockIDocumentManagementService.Setup(x => x.GetPdfBytes(It.IsAny<string>())).ReturnsAsync(pdfArray);
                mockIDocumentManagementService.Setup(x => x.ConvertHtmlToPdf(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(FakeDataGenerator.FilePath);
                mockIDocumentManagementService.Setup(x => x.ConvertHtmlToPdfVersion2(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(FakeDataGenerator.FilePath);
                mockIDocumentManagementService.Setup(x => x.AsposePdfToImages(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(FakeDataGenerator.GetPdfToImageResponse());
                //mockIDocumentManagementService.Verify(x => x.AsposePdfToImages(filePath, fileBytes, It.IsAny<string>(), "png"), Times.Once);
                return mockIDocumentManagementService;
            }
        }

        public static class IAuthenticateServiceMockUp
        {
            public static Mock<IAuthService> GetAuthenticateService()
            {
                var mockAuthenticateService = new Mock<IAuthService>();
                mockAuthenticateService.Setup(x => x.ValidateSubscriberData(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);
                mockAuthenticateService.Setup(x => x.User).Returns(FakeDataGenerator.GetUserDto());
                mockAuthenticateService.Setup(x => x.Subscriber).Returns(FakeDataGenerator.GetSubscriberDto());
                //mockAuthenticateService.Verify(x => x.ValidateSubscriberData(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
                return mockAuthenticateService;
            }
        }
    }
}


using System.Net;

namespace OpenAiBackend.Tests.UnitTests;
using NUnit.Framework.Legacy;
using OpenAiBackend.Services;
using Moq;
using Moq.Protected;
using NUnit.Framework;
public class OpenAiServiceTests
{
    private Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private HttpClient _httpClient;
    private Mock<IConfiguration> _mockConfiguration;
    private OpenAiService _service;
    private string _info;
    private string _infoSeedFilePath;
    private string _filePath;

    [SetUp]
    public void Setup()
    {
        _infoSeedFilePath = "Resources/infoSeedFile.txt";
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
        _mockConfiguration = new Mock<IConfiguration>();
        _info = File.ReadAllText("../../../"+_infoSeedFilePath);
        _service = new OpenAiService(_httpClient);
    }
    
    [Test]
    public async Task SendIrrelevantRequestToChatGpt_ShouldReturnResponse()
    {
        // Arrange
        const string apiKey = "test-api-key";
        const string expectedResponseContent =
            "{\"response\":\"I'm sorry I'm unable to answer this question as it doesn't pertain to Next Gen Forge, try another one.\"}";
        
        
        _mockConfiguration.Setup(x => x["OpenAI:ApiKey"]).Returns(apiKey);

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedResponseContent, System.Text.Encoding.UTF8, "application/json")
            });
            
        
        
        // Act
        var response = await _service.SendRequestToChatGpt(_info + "\n"+ "1+1");
        
        // Assert   
        Assert.That(response, Is.EqualTo(expectedResponseContent));
    }
    
    [TearDown]
    public void TearDown()
    {
        _httpClient.Dispose();
    }
}
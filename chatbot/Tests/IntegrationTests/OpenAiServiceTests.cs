using System.Net;
using NUnit.Framework.Legacy;
using OpenAiBackend.Services;
using Moq;
using Moq.Protected;

namespace OpenAiBackend.Tests.IntegrationTests;
using NUnit.Framework;

[TestFixture]
public class OpenAiServiceTests
{
    private OpenAiService _service;
    private string _info;
    private string _infoSeedFilePath;
    private string _filePath;

    [SetUp]
    public void Setup()
    {
        _infoSeedFilePath = "Resources/infoSeedFile.txt";
        _info = File.ReadAllText("../../../"+_infoSeedFilePath);
        _service = new OpenAiService(new HttpClient());
    }
    
    [Test]
    public async Task SendIrrelevantRequestToChatGpt_ShouldReturnResponse()
    {
        // Arrange
        const string expectedResponseContent =
            "I'm sorry I'm unable to answer this question as it doesn't pertain to Next Gen Forge, try another one.";
        
        // Act
        var response = await _service.SendRequestToChatGpt(_info + "\n"+ "QW1_112?");
        
        // Assert   
        StringAssert.Contains("sorry", response);
        StringAssert.Contains("unable", response);
        StringAssert.Contains("answer", response);
        StringAssert.Contains("question", response);
        StringAssert.Contains("another one", response);
    }
    

    [Test]
    public async Task Test_AskWhatIsTheCodeAssistanceSubmodule_ShouldReturnRelevantResponse()
    {
        // Arrange
        const string prompt = "What is the Code Assistance submodule?";
        
        // Act
        var response = await _service.SendRequestToChatGpt(_info + "\n"+ prompt);
        
        // Assert
        StringAssert.Contains("Code Assistance", response);
        StringAssert.Contains("submodule", response);
        StringAssert.Contains("code snippets", response);
    }

    [Test]
    public async Task Test_AskWhatIsTheErrorHandlingSubmodule_ShouldReturnRelevantResponse()
    {
        // Arrange
        const string prompt = "What is the Error Handling submodule?";

        // Act
        var response = await _service.SendRequestToChatGpt(_info + "\n" + prompt);

        // Assert
        StringAssert.Contains("Error Handling", response);
        StringAssert.Contains("submodule", response);
        StringAssert.Contains("coding efficiency", response);
    }
    
    [Test]
    public async Task Test_What_IsTheCodeOptimizerSubmodule_ShouldReturnRelevantResponse()
    {
        // Arrange
        const string prompt = "What is the Code Optimizer submodule?";
        
        // Act
        var response = await _service.SendRequestToChatGpt(_info + "\n"+ prompt);
        
        // Assert
        StringAssert.Contains("Code Optimizer", response);
        StringAssert.Contains("submodule", response);
        StringAssert.Contains("AI-driven algorithms", response);
        StringAssert.Contains("performance", response);
    }
    

}
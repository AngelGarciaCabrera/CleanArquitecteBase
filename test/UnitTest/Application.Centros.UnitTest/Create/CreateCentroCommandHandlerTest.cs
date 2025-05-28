using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FluentAssertions;
using Application.Centro.Create;
using Domain.Centros;
using Domain.Primitives;
using Moq;
using Domain.DomainErros;


namespace Application.Centros.UnitTest.Create;

public class CreateCentroCommandHandlerTest
{
    //Que_vamos_A_Probar
    //El_Escenario
    //Lo_que_debe_Arrojar
    private readonly Mock<ICentroRepository> _mockCentroRepository;
    // moq ayuda para que sepa que no es una instancia real
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateCentroCommandHandler _handler;
    public CreateCentroCommandHandlerTest()
    {
        _mockCentroRepository = new Mock<ICentroRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreateCentroCommandHandler(_mockCentroRepository.Object, _mockUnitOfWork.Object);

    }

    [Fact]
    public async Task HandleCreateCentro_WhenPhoneNumberHasBadFormat_ShouldReturnValidationError()
    {
        //ARRANGE
        //SE CONFIGURA LOS PARAMETOS DE ENTRADA DE NUESTRA PRUEBA UNITARIOA

        CreateCentroCommand command = new CreateCentroCommand(
            1, "TestingCentro", "AvenidaTesting", "8098765665888888888"
        );

        //ACT
        //SE EJECUTA EL METODO A PROBAR

        var res = await _handler.Handle(command, default);
        //ASSERT
        //SE VERIFICA LOS DATOS DE RETORNO DE NEUSTRO METODO FORMA
        res.IsError.Should().BeTrue();
        res.FirstError.Type.Should().Be(ErrorOr.ErrorType.Validation);
        res.FirstError.Code.Should().Be(Errors.Centro.PhoneNumberWithBadFormat.Code);
        res.FirstError.Description.Should().Be(Errors.Centro.PhoneNumberWithBadFormat.Description);

    }
}

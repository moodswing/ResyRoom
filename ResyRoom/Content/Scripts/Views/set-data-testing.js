var SetDataTesting = function (vmkoViewModel) {
    var registerEstudio = function () {
        var vm = vmkoViewModel;
        vm.koViewModel.Usuario.Nombre("Robinson Aravena");
        vm.koViewModel.Usuario.Email("rob.arav@gmail.com");
        vm.koViewModel.Usuario.Password("esurance");
        vm.koViewModel.Usuario.PasswordConfirmacion("esurance");
        vm.koViewModel.Estudio.Nombre("Noix Studio");
        vm.koViewModel.Estudio.UrlName("noixStudio");
        vm.koViewModel.Estudio.Email("noix.studio@noix.cl");
        vm.koViewModel.Estudio.Direccion("noix street 123");
        vm.koViewModel.Estudio.Salas()[0].Nombre("noix 1");
    };

    return {
        RegisterEstudio: registerEstudio
    };
}
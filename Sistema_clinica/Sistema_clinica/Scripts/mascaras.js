$(document).ready(function () {
    $('.cpf').mask("999.999.999-99", { placeholder: "___.___.___-__" });
    $('.data').mask("99/99/9999", { placeholder: "__/__/____" });
    $('.cep').mask("00000-000", { placeholder: "_____-___" });
    $('.telefone').mask("(00)00000-0000", { placeholder: "(__)_____-____" });
    $('.hora').mask("00:00", { placeholder: "__:__" });
    $('.valor').mask("00000,00", { reverse: true });
    $('.data_hora').mask("00/00/0000 00:00", { placeholder: "__/__/____ __:__" });
    $('.cpf_numeros').mask("99999999999");
})
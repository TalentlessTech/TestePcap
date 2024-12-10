using System;
using PacketDotNet;
using SharpPcap;

class Program
{
    static void Main(string[] args)
    {
        string filePath = @"C:\Temp\Pcap\Corvil-local-cne-export-1680639597609180908-1680645600235576416.pcap";

        // Abre o arquivo pcap
        var captureFile = new SharpPcap.LibPcap.CaptureFileReaderDevice(filePath);

        Console.WriteLine("Lendo pacotes do arquivo...");

        // Itera sobre os pacotes
        captureFile.OnPacketArrival += (sender, e) =>
        {
            try
            {
                // Tenta interpretar o pacote como Ethernet
                var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
                var ethPacket = (EthernetPacket)packet;
                Console.WriteLine($"Ethernet Frame: {ethPacket.SourceHardwareAddress} -> {ethPacket.DestinationHardwareAddress}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar pacote: {ex.Message}");
            }
        };

        // Inicia a leitura
        captureFile.Capture();

        Console.WriteLine("Leitura concluída.");
    }
}

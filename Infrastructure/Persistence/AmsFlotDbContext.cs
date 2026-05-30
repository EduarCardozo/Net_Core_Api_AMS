using Microsoft.EntityFrameworkCore;
using Net_Core_Api.Domain.Entities;

namespace Net_Core_Api.Infrastructure.Persistence;

public class AmsFlotDbContext : DbContext
{
    public AmsFlotDbContext(DbContextOptions<AmsFlotDbContext> options) : base(options) { }

    public DbSet<Alarmas> Alarmas { get; set; }
    public DbSet<AlarmasConfiguraciones> AlarmasConfiguraciones { get; set; }
    public DbSet<CO_UnidadesMedida> CO_UnidadesMedida { get; set; }
    public DbSet<Comentarios> Comentarios { get; set; }
    public DbSet<Configuraciones> Configuraciones { get; set; }
    public DbSet<Connectors> Connectors { get; set; }
    public DbSet<CONST_SistemasExternosEntidadesCodigos_TargetType> CONST_SistemasExternosEntidadesCodigos_TargetType { get; set; }
    public DbSet<Depositos> Depositos { get; set; }
    public DbSet<DocumentosElectronicos> DocumentosElectronicos { get; set; }
    public DbSet<Equipos> Equipos { get; set; }
    public DbSet<EquiposGrupos> EquiposGrupos { get; set; }
    public DbSet<EquiposMediciones> EquiposMediciones { get; set; }
    public DbSet<EquiposPartesItems> EquiposPartesItems { get; set; }
    public DbSet<EquiposTipos> EquiposTipos { get; set; }
    public DbSet<Especialidades> Especialidades { get; set; }
    public DbSet<G_Direcciones> G_Direcciones { get; set; }
    public DbSet<G_DireccionesOwners> G_DireccionesOwners { get; set; }
    public DbSet<G_Localidades> G_Localidades { get; set; }
    public DbSet<G_Paises> G_Paises { get; set; }
    public DbSet<G_Regiones> G_Regiones { get; set; }
    public DbSet<G_SubRegiones> G_SubRegiones { get; set; }
    public DbSet<Items> Items { get; set; }
    public DbSet<Licencias> Licencias { get; set; }
    public DbSet<LicenciasLogs> LicenciasLogs { get; set; }
    public DbSet<LicenciasMoviles> LicenciasMoviles { get; set; }
    public DbSet<Marcas> Marcas { get; set; }
    public DbSet<MarcasModelos> MarcasModelos { get; set; }
    public DbSet<Monedas> Monedas { get; set; }
    public DbSet<NovedadesTecnicas> NovedadesTecnicas { get; set; }
    public DbSet<NovedadesTecnicasItems> NovedadesTecnicasItems { get; set; }
    public DbSet<OrdenesTrabajo> OrdenesTrabajo { get; set; }
    public DbSet<OrdenesTrabajoDetalles> OrdenesTrabajoDetalles { get; set; }
    public DbSet<OrdenesTrabajoOperaciones> OrdenesTrabajoOperaciones { get; set; }
    public DbSet<OrdenesTrabajoPartes> OrdenesTrabajoPartes { get; set; }
    public DbSet<OrdenesTrabajoPartesItems> OrdenesTrabajoPartesItems { get; set; }
    public DbSet<OrdenesTrabajoTareas> OrdenesTrabajoTareas { get; set; }
    public DbSet<OrdenesTrabajoUsuarios> OrdenesTrabajoUsuarios { get; set; }
    public DbSet<Partes> Partes { get; set; }
    public DbSet<PartesCategorias> PartesCategorias { get; set; }
    public DbSet<PartesItems> PartesItems { get; set; }
    public DbSet<PartesItemsOrdenesReparacion> PartesItemsOrdenesReparacion { get; set; }
    public DbSet<PartesItemsRemitos> PartesItemsRemitos { get; set; }
    public DbSet<PartesStockMovimientos> PartesStockMovimientos { get; set; }
    public DbSet<PartesStocks> PartesStocks { get; set; }
    public DbSet<Perfiles> Perfiles { get; set; }
    public DbSet<PerfilesRoles> PerfilesRoles { get; set; }
    public DbSet<PerfilesURLTargets> PerfilesURLTargets { get; set; }
    public DbSet<PlanesMantenimiento> PlanesMantenimiento { get; set; }
    public DbSet<PlanesMantenimientoOrdenesTrabajo> PlanesMantenimientoOrdenesTrabajo { get; set; }
    public DbSet<PlanesMantenimientoOrdenesTrabajoEjecuciones> PlanesMantenimientoOrdenesTrabajoEjecuciones { get; set; }
    public DbSet<Plantillas> Plantillas { get; set; }
    public DbSet<PlantillasItems> PlantillasItems { get; set; }
    public DbSet<PlantillasTipos> PlantillasTipos { get; set; }
    public DbSet<ProductVersion> ProductVersion { get; set; }
    public DbSet<Proveedores> Proveedores { get; set; }
    public DbSet<Remitos> Remitos { get; set; }
    public DbSet<RemitosPartes> RemitosPartes { get; set; }
    public DbSet<RES_Feriados> RES_Feriados { get; set; }
    public DbSet<RES_Novedades> RES_Novedades { get; set; }
    public DbSet<RES_NovedadesOrdenesTrabajo> RES_NovedadesOrdenesTrabajo { get; set; }
    public DbSet<RES_NovedadesPlanesMantenimiento> RES_NovedadesPlanesMantenimiento { get; set; }
    public DbSet<RES_NovedadesReemplazos> RES_NovedadesReemplazos { get; set; }
    public DbSet<RES_NovedadesTipos> RES_NovedadesTipos { get; set; }
    public DbSet<RES_Turnos> RES_Turnos { get; set; }
    public DbSet<RES_TurnosLocaciones> RES_TurnosLocaciones { get; set; }
    public DbSet<RES_TurnosRecursos> RES_TurnosRecursos { get; set; }
    public DbSet<Revisiones> Revisiones { get; set; }
    public DbSet<RevisionesAttachments> RevisionesAttachments { get; set; }
    public DbSet<RevisionesEstados> RevisionesEstados { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<Rubros> Rubros { get; set; }
    public DbSet<S_Prestadores> S_Prestadores { get; set; }
    public DbSet<S_Prestadores_PrestadoresCategorias> S_Prestadores_PrestadoresCategorias { get; set; }
    public DbSet<S_PrestadoresCategorias> S_PrestadoresCategorias { get; set; }
    public DbSet<S_Servicios> S_Servicios { get; set; }
    public DbSet<S_ServiciosCategorias> S_ServiciosCategorias { get; set; }
    public DbSet<S_ServiciosPrestadores> S_ServiciosPrestadores { get; set; }
    public DbSet<SistemasExternos> SistemasExternos { get; set; }
    public DbSet<SistemasExternosEntidadesCodigos> SistemasExternosEntidadesCodigos { get; set; }
    public DbSet<Talleres> Talleres { get; set; }
    public DbSet<Tareas> Tareas { get; set; }
    public DbSet<TareasItems> TareasItems { get; set; }
    public DbSet<TareasPartes> TareasPartes { get; set; }
    public DbSet<TAX_AdministradoresTributarios> TAX_AdministradoresTributarios { get; set; }
    public DbSet<TAX_AdministradoresTributariosActividadesEconomicas> TAX_AdministradoresTributariosActividadesEconomicas { get; set; }
    public DbSet<TAX_DocumentosTalonarios> TAX_DocumentosTalonarios { get; set; }
    public DbSet<TAX_DocumentosTalonariosTipos> TAX_DocumentosTalonariosTipos { get; set; }
    public DbSet<TAX_DocumentosTipos> TAX_DocumentosTipos { get; set; }
    public DbSet<TAX_Impuestos> TAX_Impuestos { get; set; }
    public DbSet<TAX_ImpuestosAlicuotas> TAX_ImpuestosAlicuotas { get; set; }
    public DbSet<TAX_ImpuestosCategorias> TAX_ImpuestosCategorias { get; set; }
    public DbSet<TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos> TAX_ImpuestosCategoriasDocumentosTiposTalonariosTipos { get; set; }
    public DbSet<TAX_ImpuestosRoles> TAX_ImpuestosRoles { get; set; }
    public DbSet<TAX_ImpuestosTimbrados> TAX_ImpuestosTimbrados { get; set; }
    public DbSet<TAX_PersonasJuridicasTipos> TAX_PersonasJuridicasTipos { get; set; }
    public DbSet<TAX_PersonasTipos> TAX_PersonasTipos { get; set; }
    public DbSet<TAX_RegimenesTributarios> TAX_RegimenesTributarios { get; set; }
    public DbSet<TAX_RegimenesTributariosImpuestosCategorias> TAX_RegimenesTributariosImpuestosCategorias { get; set; }
    public DbSet<TAX_RegimenesTributariosS_Prestadores> TAX_RegimenesTributariosS_Prestadores { get; set; }
    public DbSet<TFC_RevisionesConfig> TFC_RevisionesConfig { get; set; }
    public DbSet<URLTargets> URLTargets { get; set; }
    public DbSet<URLTargetsRoles> URLTargetsRoles { get; set; }
    public DbSet<Usuarios> Usuarios { get; set; }
    public DbSet<UsuariosEspecialidades> UsuariosEspecialidades { get; set; }
    public DbSet<UsuariosPerfiles> UsuariosPerfiles { get; set; }
    public DbSet<UsuariosRelaciones> UsuariosRelaciones { get; set; }
    public DbSet<UsuariosRoles> UsuariosRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EquiposPartesItems>()
            .HasKey(e => new { e.EquipoID, e.ParteItemID });

        modelBuilder.Entity<G_DireccionesOwners>()
            .HasKey(e => new { e.DireccionTipo, e.DireccionID, e.OwnerType, e.OwnerID });

        modelBuilder.Entity<NovedadesTecnicasItems>()
            .HasKey(e => new { e.NovedadTecnicaID, e.PlantillaID, e.ItemID });

        modelBuilder.Entity<OrdenesTrabajoPartes>()
            .HasKey(e => new { e.OrdenTrabajoID, e.ParteID });

        modelBuilder.Entity<OrdenesTrabajoPartesItems>()
            .HasKey(e => new { e.OrdenTrabajoID, e.ParteItemID });

        modelBuilder.Entity<OrdenesTrabajoTareas>()
            .HasKey(e => new { e.OrdenTrabajoID, e.TareaID, e.EspecialidadID });

        modelBuilder.Entity<OrdenesTrabajoUsuarios>()
            .HasKey(e => new { e.OrdenTrabajoID, e.UsuarioID });

        modelBuilder.Entity<PartesItemsRemitos>()
            .HasKey(e => new { e.ParteItemID, e.RemitoID });

        modelBuilder.Entity<PerfilesRoles>()
            .HasKey(e => new { e.PerfilID, e.RolID });

        modelBuilder.Entity<PerfilesURLTargets>()
            .HasKey(e => new { e.PerfilID, e.URLTargetID });

        modelBuilder.Entity<PlantillasItems>()
            .HasKey(e => new { e.PlantillaID, e.ItemID });

        modelBuilder.Entity<RemitosPartes>()
            .HasKey(e => new { e.RemitoID, e.ParteID });

        modelBuilder.Entity<RevisionesAttachments>()
            .HasKey(e => new { e.RevisionID, e.Type });

        modelBuilder.Entity<RevisionesEstados>()
            .HasKey(e => new { e.RevisionID, e.PlantillaID, e.ItemID });

        modelBuilder.Entity<S_Prestadores_PrestadoresCategorias>()
            .HasKey(e => new { e.PrestadorId, e.PrestadorCategoriaId });

        modelBuilder.Entity<TareasItems>()
            .HasKey(e => new { e.TareaID, e.ItemID });

        modelBuilder.Entity<TareasPartes>()
            .HasKey(e => new { e.TareaID, e.ParteID });

        modelBuilder.Entity<URLTargetsRoles>()
            .HasKey(e => new { e.URLTargetID, e.RolID });

        modelBuilder.Entity<UsuariosEspecialidades>()
            .HasKey(e => new { e.EspecialidadID, e.UsuarioID });

        modelBuilder.Entity<UsuariosPerfiles>()
            .HasKey(e => new { e.UsuarioID, e.PerfilID });

        modelBuilder.Entity<UsuariosRelaciones>()
            .HasKey(e => new { e.UsuarioID, e.RelacionTipo, e.RelacionID });

        modelBuilder.Entity<UsuariosRoles>()
            .HasKey(e => new { e.Usuario, e.Rol });
    }
}

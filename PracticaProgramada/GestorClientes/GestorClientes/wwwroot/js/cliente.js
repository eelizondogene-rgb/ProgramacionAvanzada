// Pantalla de clientes: tabla, formulario (crear/editar), detalle, borrado y mensajes.
// Consume la API REST: /api/clientes
(function () {
    'use strict';

    const urlApi = '/api/clientes';
    const longitudTelefono = 8;

    let tabla = null;
    let clienteIdPorBorrar = null;

    // ---------- Referencias al DOM ----------
    const elModalDetalle = document.getElementById('modalDetalle');
    const elModalFormulario = document.getElementById('modalFormulario');
    const elModalBorrar = document.getElementById('modalBorrar');
    const elToast = document.getElementById('toastMensaje');

    const modalDetalle = new bootstrap.Modal(elModalDetalle);
    const modalFormulario = new bootstrap.Modal(elModalFormulario);
    const modalBorrar = new bootstrap.Modal(elModalBorrar);
    const toast = new bootstrap.Toast(elToast, { delay: 4000 });

    const formulario = document.getElementById('formCliente');
    const listaTelefonos = document.getElementById('listaTelefonos');
    const alertaFormulario = document.getElementById('alertaFormulario');
    const btnGuardar = document.getElementById('btnGuardar');
    const btnConfirmarBorrar = document.getElementById('btnConfirmarBorrar');

    // ---------- Utilidades ----------
    function escaparHtml(texto) {
        const div = document.createElement('div');
        div.textContent = texto == null ? '' : String(texto);
        return div.innerHTML;
    }

    function mostrarMensaje(texto, esExito) {
        elToast.classList.remove('text-bg-success', 'text-bg-danger');
        elToast.classList.add(esExito ? 'text-bg-success' : 'text-bg-danger');
        document.getElementById('toastTexto').textContent = texto;
        toast.show();
    }

    // Llama a la API y devuelve { ok, datos, mensaje }. Nunca lanza excepción.
    async function llamarApi(url, opciones) {
        try {
            const respuesta = await fetch(url, opciones);
            let datos = null;
            try {
                datos = await respuesta.json();
            } catch (e) {
                datos = null; // respuesta sin cuerpo (por ejemplo 404)
            }

            if (respuesta.ok) {
                return { ok: true, datos: datos, mensaje: datos && datos.mensaje };
            }

            let mensaje = (datos && (datos.mensaje || datos.Mensaje || datos.title)) || null;
            if (!mensaje && respuesta.status === 404) {
                mensaje = 'No se encontró el cliente.';
            }
            return { ok: false, datos: datos, mensaje: mensaje || 'Ocurrió un error inesperado (código ' + respuesta.status + ').' };
        } catch (error) {
            return { ok: false, datos: null, mensaje: 'No se pudo conectar con el servidor.' };
        }
    }

    // ---------- Tabla ----------
    function inicializarTabla() {
        tabla = new DataTable('#tablaClientes', {
            columns: [
                { data: 'cedula', render: function (d) { return escaparHtml(d); } },
                { data: 'nombre', render: function (d) { return escaparHtml(d); } },
                { data: 'apellidos', render: function (d) { return escaparHtml(d); } },
                {
                    data: 'telefonos',
                    orderable: false,
                    render: function (telefonos) {
                        if (!telefonos || telefonos.length === 0) {
                            return '<span class="text-muted">Sin teléfonos</span>';
                        }
                        return telefonos
                            .map(function (t) { return '<span class="badge text-bg-light border me-1">' + escaparHtml(t.numero) + '</span>'; })
                            .join('');
                    }
                },
                {
                    data: 'id',
                    orderable: false,
                    searchable: false,
                    className: 'text-center',
                    render: function (id) {
                        return '<div class="btn-group btn-group-sm" role="group">' +
                            '<button type="button" class="btn btn-outline-primary btn-ver" data-id="' + id + '" title="Ver"><i class="bi bi-eye"></i></button>' +
                            '<button type="button" class="btn btn-outline-secondary btn-editar" data-id="' + id + '" title="Editar"><i class="bi bi-pencil"></i></button>' +
                            '<button type="button" class="btn btn-outline-danger btn-borrar" data-id="' + id + '" title="Borrar"><i class="bi bi-trash"></i></button>' +
                            '</div>';
                    }
                }
            ],
            language: {
                search: 'Buscar:',
                lengthMenu: 'Mostrar _MENU_ registros',
                info: 'Mostrando _START_ a _END_ de _TOTAL_ clientes',
                infoEmpty: 'No hay clientes',
                infoFiltered: '(filtrado de _MAX_ clientes)',
                emptyTable: 'No hay clientes registrados',
                zeroRecords: 'No se encontraron clientes',
                paginate: { first: 'Primero', last: 'Último', next: 'Siguiente', previous: 'Anterior' }
            }
        });
    }

    async function cargarClientes() {
        const resultado = await llamarApi(urlApi);
        if (!resultado.ok) {
            mostrarMensaje(resultado.mensaje, false);
            return;
        }
        const clientes = (resultado.datos && resultado.datos.dato) || [];
        tabla.clear();
        tabla.rows.add(clientes);
        tabla.draw();
    }

    // ---------- Detalle ----------
    async function verDetalle(id) {
        const resultado = await llamarApi(urlApi + '/' + id);
        if (!resultado.ok) {
            mostrarMensaje(resultado.mensaje, false);
            return;
        }
        const cliente = resultado.datos.dato;
        document.getElementById('detalleCedula').textContent = cliente.cedula;
        document.getElementById('detalleNombre').textContent = cliente.nombre;
        document.getElementById('detalleApellidos').textContent = cliente.apellidos || '-';

        const contenedor = document.getElementById('detalleTelefonos');
        contenedor.innerHTML = '';
        if (!cliente.telefonos || cliente.telefonos.length === 0) {
            contenedor.textContent = 'Sin teléfonos';
        } else {
            cliente.telefonos.forEach(function (t) {
                const linea = document.createElement('div');
                linea.innerHTML = '<i class="bi bi-telephone me-1"></i>' + escaparHtml(t.numero);
                contenedor.appendChild(linea);
            });
        }
        modalDetalle.show();
    }

    // ---------- Formulario: teléfonos dinámicos ----------
    function agregarFilaTelefono(numero) {
        const fila = document.createElement('div');
        fila.className = 'input-group mb-2 fila-telefono';
        fila.innerHTML =
            '<span class="input-group-text"><i class="bi bi-telephone"></i></span>' +
            '<input type="text" class="form-control input-telefono" inputmode="numeric" maxlength="' + longitudTelefono + '" placeholder="8 dígitos" autocomplete="off" />' +
            '<button type="button" class="btn btn-outline-danger btn-quitar-telefono" title="Quitar teléfono"><i class="bi bi-x-lg"></i></button>' +
            '<div class="invalid-feedback">Debe tener exactamente ' + longitudTelefono + ' dígitos.</div>';
        fila.querySelector('.input-telefono').value = numero || '';
        listaTelefonos.appendChild(fila);
        actualizarBotonesQuitar();
    }

    // Siempre debe quedar al menos un teléfono.
    function actualizarBotonesQuitar() {
        const filas = listaTelefonos.querySelectorAll('.fila-telefono');
        filas.forEach(function (fila) {
            fila.querySelector('.btn-quitar-telefono').disabled = filas.length === 1;
        });
    }

    function limpiarFormulario() {
        formulario.reset();
        formulario.querySelectorAll('.is-invalid').forEach(function (el) { el.classList.remove('is-invalid'); });
        document.getElementById('clienteId').value = '0';
        alertaFormulario.classList.add('d-none');
        listaTelefonos.innerHTML = '';
    }

    function abrirFormularioNuevo() {
        limpiarFormulario();
        document.getElementById('tituloFormulario').textContent = 'Nuevo cliente';
        agregarFilaTelefono('');
        modalFormulario.show();
    }

    async function abrirFormularioEditar(id) {
        const resultado = await llamarApi(urlApi + '/' + id);
        if (!resultado.ok) {
            mostrarMensaje(resultado.mensaje, false);
            return;
        }
        const cliente = resultado.datos.dato;
        limpiarFormulario();
        document.getElementById('tituloFormulario').textContent = 'Editar cliente';
        document.getElementById('clienteId').value = cliente.id;
        document.getElementById('cedula').value = cliente.cedula;
        document.getElementById('nombre').value = cliente.nombre;
        document.getElementById('apellidos').value = cliente.apellidos || '';

        if (cliente.telefonos && cliente.telefonos.length > 0) {
            cliente.telefonos.forEach(function (t) { agregarFilaTelefono(t.numero); });
        } else {
            agregarFilaTelefono('');
        }
        modalFormulario.show();
    }

    function marcarCampo(elemento, esValido) {
        elemento.classList.toggle('is-invalid', !esValido);
        return esValido;
    }

    function validarFormulario() {
        let esValido = true;
        esValido = marcarCampo(document.getElementById('cedula'), document.getElementById('cedula').value.trim() !== '') && esValido;
        esValido = marcarCampo(document.getElementById('nombre'), document.getElementById('nombre').value.trim() !== '') && esValido;

        const expresion = new RegExp('^\\d{' + longitudTelefono + '}$');
        listaTelefonos.querySelectorAll('.input-telefono').forEach(function (input) {
            esValido = marcarCampo(input, expresion.test(input.value.trim())) && esValido;
        });
        return esValido;
    }

    async function guardarCliente(evento) {
        evento.preventDefault();
        alertaFormulario.classList.add('d-none');

        if (!validarFormulario()) {
            return;
        }

        const id = parseInt(document.getElementById('clienteId').value, 10) || 0;
        const esEdicion = id > 0;

        const cliente = {
            id: id,
            cedula: document.getElementById('cedula').value.trim(),
            nombre: document.getElementById('nombre').value.trim(),
            apellidos: document.getElementById('apellidos').value.trim(),
            telefonos: Array.from(listaTelefonos.querySelectorAll('.input-telefono')).map(function (input) {
                return { id: 0, numero: input.value.trim() };
            })
        };

        btnGuardar.disabled = true;
        const resultado = await llamarApi(esEdicion ? urlApi + '/' + id : urlApi, {
            method: esEdicion ? 'PUT' : 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(cliente)
        });
        btnGuardar.disabled = false;

        if (!resultado.ok) {
            alertaFormulario.textContent = resultado.mensaje;
            alertaFormulario.classList.remove('d-none');
            return;
        }

        modalFormulario.hide();
        mostrarMensaje(esEdicion ? 'Cliente actualizado correctamente.' : 'Cliente creado correctamente.', true);
        await cargarClientes();
    }

    // ---------- Borrar ----------
    function pedirConfirmacionBorrado(id) {
        const fila = tabla.rows().data().toArray().find(function (c) { return c.id === id; });
        clienteIdPorBorrar = id;
        document.getElementById('nombreBorrar').textContent = fila ? (fila.nombre + ' ' + fila.apellidos).trim() : 'este cliente';
        modalBorrar.show();
    }

    async function borrarCliente() {
        if (clienteIdPorBorrar === null) {
            return;
        }
        btnConfirmarBorrar.disabled = true;
        const resultado = await llamarApi(urlApi + '/' + clienteIdPorBorrar, { method: 'DELETE' });
        btnConfirmarBorrar.disabled = false;
        modalBorrar.hide();
        clienteIdPorBorrar = null;

        if (!resultado.ok) {
            mostrarMensaje(resultado.mensaje, false);
            return;
        }
        mostrarMensaje('Cliente borrado correctamente.', true);
        await cargarClientes();
    }

    // ---------- Eventos ----------
    document.getElementById('btnNuevoCliente').addEventListener('click', abrirFormularioNuevo);
    document.getElementById('btnAgregarTelefono').addEventListener('click', function () { agregarFilaTelefono(''); });
    formulario.addEventListener('submit', guardarCliente);
    btnConfirmarBorrar.addEventListener('click', borrarCliente);

    // Botón X de cada teléfono y limpieza de errores al escribir.
    listaTelefonos.addEventListener('click', function (evento) {
        const boton = evento.target.closest('.btn-quitar-telefono');
        if (boton && !boton.disabled) {
            boton.closest('.fila-telefono').remove();
            actualizarBotonesQuitar();
        }
    });

    // Los teléfonos solo aceptan dígitos.
    listaTelefonos.addEventListener('input', function (evento) {
        if (evento.target.classList.contains('input-telefono')) {
            evento.target.value = evento.target.value.replace(/\D/g, '');
            evento.target.classList.remove('is-invalid');
        }
    });
    ['cedula', 'nombre'].forEach(function (idCampo) {
        document.getElementById(idCampo).addEventListener('input', function (evento) {
            evento.target.classList.remove('is-invalid');
        });
    });

    // Botones por fila (ver, editar, borrar).
    document.getElementById('tablaClientes').addEventListener('click', function (evento) {
        const boton = evento.target.closest('button[data-id]');
        if (!boton) {
            return;
        }
        const id = parseInt(boton.dataset.id, 10);
        if (boton.classList.contains('btn-ver')) {
            verDetalle(id);
        } else if (boton.classList.contains('btn-editar')) {
            abrirFormularioEditar(id);
        } else if (boton.classList.contains('btn-borrar')) {
            pedirConfirmacionBorrado(id);
        }
    });

    // ---------- Inicio ----------
    inicializarTabla();
    cargarClientes();
})();

import React, { Component } from 'react';
import axios from "axios";
//const $ = require("jquery");
//require("datatables.net")(window, $);

export class HouseList extends Component {
    static displayName = HouseList.name;

    constructor(props) {
        super(props);
        this.state = {
            houses: {}, loading: true
        };
    }

    componentDidMount() {
        this.populateData();
    }
    static renderTable(houses) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Evin Sahipleri</th>
                        <th>Evin Durumu</th>
                        <th>Blok Adı</th>
                        <th>Kat</th>
                        <th>Kapı No</th>

                    </tr>
                </thead>
                <tbody>
                    {houses.isSuccess ? (houses.entityList.map(h =>
                        <tr>
                            <td>{h.id}</td>
                            <td>{h.OwnerDisplayName}</td>
                            <td>{h.IsActive}</td>
                            <td>{h.BlockName}</td>
                            <td>{h.FloorNumber}</td>
                            <td>{h.DoorNumber}</td>
                        </tr>
                    )) : (<tr><td colSpan="6" className="text-center">{houses.exeptionMessage}</td></tr>)}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Yükleniyor...</em></p>
            : HouseList.renderTable(this.state.houses);

        return (
            <div>
                <h1 className="display-4">Evler</h1>
                {contents}
            </div>
        );
    }

    async populateData() {
        var resp = await axios.get("Houses");
        this.setState({ houses: resp.data, loading: false });
    }
}
    //syncTable() {
    //    this.$el = $(this.el);
    //    this.$el.DataTable({
    //        data: this.state.houses.entityList,
    //        columns: [
    //            { title: "Id", data: "Id" },
    //            { title: "Evin Sahipleri", data: "OwnerDisplayName" },
    //            { title: "Evin Durumu", data: "IsActive" },
    //            { title: "Blok Adı", data: "BlockName" },
    //            { title: "Kat", data: "FloorNumber" },
    //            { title: "Kapı No", data: "DoorNumber" }
    //        ]
    //    });
    //}

/*<table className="table table-striped table-hovered w-100" ref={(el) => (this.el = el)}>*/
/*</table>*/
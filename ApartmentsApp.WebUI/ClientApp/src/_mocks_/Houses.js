import React, { useEffect, useState } from "react";
import axios from "axios";

export default function Houses() {
    const [house, setHouse] = useState([]);
    useEffect(() => {
        axios("Houses")
            .then((res) => setHouse(res.data))
            .catch((e) => console.log(e))
    }, []);

    return (
        <>
            {house}
        </>
    )
}
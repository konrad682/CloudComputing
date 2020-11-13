import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
      this.state = { shopTraffics: [], loading: true };
      this.populateWeatherData(1);
  }

  static renderForecastsTable(shopTraffics) {
        return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Shop name</th>
            <th>People in</th>
            <th>People out</th>
            <th>People acctual</th>
          </tr>
        </thead>
        <tbody>
                {shopTraffics.map(shopTraffic =>
              <tr key={shopTraffic.id}>
                  <td>{shopTraffic.date}</td>
                  <td>{shopTraffic.shopId}</td>
                  <td>{shopTraffic.peopleIn}</td>
                  <td>{shopTraffic.peopleOut}</td>
                  <td>{shopTraffic.peopleActual}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : FetchData.renderForecastsTable(this.state.shopTraffics);

    return (
      <div>
        <h1 id="tabelLabel" >Shop infromation</h1>
            <p>This component demonstrates fetching data from the server.</p> 
                <div class="btn-group" role="group" aria-label="Basic example">
                    <button type="button" onClick={() => this.populateWeatherData(1)}className="btn btn-secondary">Last minute</button>
                    <button type="button" onClick={() => this.populateWeatherData(2)} className="btn btn-secondary">Last hours</button>
                    <button type="button" onClick={() => this.populateWeatherData(3)} className="btn btn-secondary">Last 4 hours</button>
                    <button type="button" onClick={() => this.populateWeatherData(4)} className="btn btn-secondary">Last day</button>
                        <button type="button" onClick={() => this.populateWeatherData(5)} className="btn btn-secondary">Last week</button>
                </div>
        {contents}
        </div>
    );
  }

    async populateWeatherData(optionValue)
    {
        const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ option: optionValue})
    };
    const response = await fetch('weatherforecast/all', requestOptions);
    const data = await response.json();
      this.setState({ shopTraffics: data, loading: false });
  }
}

const mongoose = require('mongoose');

const connectDb = async () => {
    try{
        const MONGO_URI = process.env.MONGO_URI;
        await mongoose.connect(MONGO_URI)
        .then(() => console.log('Database connected'))
        .catch(err => console.log('ERROR: ',err));
    } catch(err){
        console.log('Unable to connect to database.',err);
    }
}

module.exports = connectDb;